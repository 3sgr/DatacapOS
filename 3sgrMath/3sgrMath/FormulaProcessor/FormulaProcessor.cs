using System;
using System.Collections.Generic;
using System.Linq;
using _3sgrMath.Properties;

namespace _3sgrMath
{
    public class FormulaProcessor
    {
        private readonly ActionsAndFunctions _af;
        public FormulaProcessor()
        {
            _af = new ActionsAndFunctions();
        }

        private Dictionary<string, Func<string, double>> _customFunctions;
        public FormulaProcessor(Dictionary<string, Func<string, double>> customFunctions)
        {
            _customFunctions = customFunctions;
            _af = new ActionsAndFunctions(_customFunctions);
        }

        private readonly Stack<int> _stackBrackets = new Stack<int>(0);
        private readonly Dictionary<string, double> _iDic = new Dictionary<string, double>();
        //       private Dictionary<string, double> _CDic = new Dictionary<string, double>();
        private readonly Dictionary<int, int> _subStrings = new Dictionary<int, int>();

        private readonly char[] _cc0 = Templates.Separators.ToCharArray();

        private readonly char[] _cc1 = Templates.ArithmeticOperations.ToCharArray();

        private readonly char[] _cc2 = Templates.ForbiddenSymbols.ToCharArray();

        private int _newIdN = 1;
        private readonly string _newIdS = "?";
        // The end of the Class

        #region Twins&Functions

        private readonly string[,] _lTwins =
        {
            {"(+", "("}, {"(-", "(0-"}, {"*+", "*"}, {"*-", "*(-1)*"}, {"^+", "^"},
            {"++", "+"}, {"-+", "-"}, {"+-", "-"}
        };

        private readonly string[] _functionsID = {"sqrt", "ln", "exp", "abs", "sin", "cos", "asin", "acos"};

        #endregion

        #region UserException

        public class ExceptionMesage : Exception
        {
            public ExceptionMesage()
            {
            }

            public ExceptionMesage(string message) : base(message)
            {
            }
        }

        
        #endregion
        #region Main
        /// <summary>
        ///     The main executable method (module)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Value(string value)
        {
            var val = LegalTwinsCorrection(value);
            SelfDetermination(val);
            if (!SynControl(val)) throw new ExceptionMesage(ErrorEx.Syntax);
            return SentenceAnalysis(val);
        }
        /// <summary>
        ///     Syntaxis Control section
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SynControl(string value)
        {
            var sO = Templates.Eliminator + value + Templates.Eliminator;
            // Balance parentheses and filling of a collection of the substring  areas(first - last) in brackets
            if (!ParentsesesCount(value)) return false;
            // Detection of a prohibited symbol
            if (_cc2.Any(ch => value.IndexOf(ch) >= 0))
            {
                return false;
            }
            // Remove ArithmeticOperations
            CharsReplace(_cc0, ref sO, Templates.Eliminator);
            CharsReplace(_cc1, ref sO, Templates.Eliminator);
            // Remove ID
            ICollection<string> keys = _iDic.Keys;
            sO = keys.Aggregate(sO, (current, s) => current.Replace(Templates.Eliminator[0] + s + Templates.Eliminator[1], Templates.Empty));
            // Remove Eliminator
            CharsReplace(new[] {Templates.Eliminator[1]}, ref sO);
            return sO == Templates.Empty||_customFunctions.Count>0;
            return sO == Templates.Empty || _customFunctions.Count > 0;
        }

        /// <summary>
        ///     Completing the collection values of self-defined identifiers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SelfDetermination(string value)
        {
            int first = 0, last = 0;
            while (last < value.Length)
            {
                first = SignFinder(value, first);
                last = SignFinder(value, first + 1);
                var sBs = value.Substring(first + 1, last - first - 1);
                try
                {
                    var val = double.Parse(sBs);
                    try
                    {
                        _iDic.Add(sBs, val);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
                catch (FormatException)
                {
                }
                first = last;
            }
            foreach (var customName in _customFunctions.Keys)
            {
                _iDic.Add(customName,0.0);
            }
        }
        /// <summary>
        ///     Filling the collections that define the procedure for computing the formula
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double SentenceAnalysis(string value)
        {
            string key, zone = value;
            ICollection<int> keys = _subStrings.Keys;
            var first = keys.First();
            while (true)
            {
                var last = _subStrings[first];
                var sBs = zone.Substring(first, last - first + 1);
                key = ParenthesesToNumbers(sBs);
                FormylaChanging(first, key, ref sBs, ref zone);
                if (!ParentsesesCount(zone)) throw new ExceptionMesage(ErrorEx.Fatal);
                keys = _subStrings.Keys;
                if (0 == keys.Count) break;
                first = keys.First();
            }
            return _iDic[key.Substring(1, key.Length - 2)];
        }

        /// <summary>
        ///     "Convolution" of the parentheses
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ParenthesesToNumbers(string value)
        {
            var sBs = value;
            _af.ParametersClear();
            for (var i = 0; i < 5; i++)
            {
// For all the signs of arithmetic operations in accordance with the priority
                int center = 0, first = 0;
                while (center >= 0 && center < sBs.Length)

                {
// To the left and right of the operator sign
                    center = SignFinder(sBs, center + 1, _cc1[i]);
                    if (center < 0) continue;
                    int last;
                    while (true)
                    {
                        first = SignFinder(sBs, first);
                        last = SignFinder(sBs, first + 1);
                        if (last == center) break;
                        first = last;
                    }
                    // The left and right operands are allocated
                    var l = sBs.Substring(first + 1, last - first - 1);
                    last = SignFinder(sBs, center + 1);
                    var r = sBs.Substring(center + 1, last - center - 1);
                    if (l == Templates.Empty || l == Templates.Empty) throw new ExceptionMesage(ErrorEx.NoDat);
                    // Operands into stack
                    _af.ParametersPush(_iDic[l]);
                    _af.ParametersPush(_iDic[r]);
                    var val = ArithmeticExpression(i); // Executining
                    AddID(val);
                    sBs = sBs.Replace(sBs.Substring(first + 1, last - first - 1), _newIdS + _newIdN);
                        // Correction subformulae as a  "convolution" operation
                    _newIdN++;
                    center = 0;
                    first = 0;
                }
            }
            return sBs;
        }

        /// <summary>
        ///     Changing the  Formylas after parentheses convolution
        /// </summary>
        /// <param name="first"></param>
        /// <param name="key"></param>
        /// <param name="sBs"></param>
        /// <param name="zone"></param>
        public void FormylaChanging(int first, string key, ref string sBs, ref string zone)
        {
            var rez = key;
            var svch = true;
            try
            {
                rez = rez.Substring(1, key.Length - 2);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ExceptionMesage(ErrorEx.Fatal);
            }
            if (0 != first)
            {
                for (var i = 0; i < 6; i++)
                    if (zone[first - 1] == _cc1[i])
                    {
                        svch = false;
                        break;
                    }
                if (svch) rez = FunctionsProcessor(first, rez, ref sBs, zone);
            }
            zone = zone.Replace(sBs, rez);
        }

        /// <summary>
        ///     Calculating the value of the function of the collection of ID-value with an argument that has been calculated in
        ///     brackets above
        /// </summary>
        /// <param name="first"></param>
        /// <param name="rez"></param>
        /// <param name="sBs"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        public string FunctionsProcessor(int first, string rez, ref string sBs, string zone)
        {
            var funcID = Templates.Empty;
            ICollection<string> keys = _iDic.Keys;
            foreach (var s in from s in keys let ind = zone.IndexOf(s + "(", StringComparison.Ordinal) where ind >= 0 select s)
            {
                funcID = s;
                break;
            }
            if (funcID != Templates.Empty)
            {
                _af.ParametersClear();
                _af.ParametersPush(_iDic[funcID]);
                _af.ParametersPush(_iDic[rez]);
                rez = _newIdS + _newIdN;
                sBs = funcID + sBs;
                AddID(Functions(funcID));
                _newIdN++;
            }
            else throw new ExceptionMesage(ErrorEx.NoFunc);
            return rez;
        }

        #endregion

        #region Sevice

        /// <summary>
        ///     Puting the element on the key for the ID-value Dictionary (private)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        public void InitID(string k, double v)
        {
            _iDic.Add(k, v);
        }

        /// <summary>
        ///     Getting the element on the key for the ID-value Dictionary (private)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double FindNumber(string key)
        {
            ICollection<string> keys = _iDic.Keys;
            foreach (var s in keys.Where(s => s == key))
            {
                return _iDic[s];
            }
            throw new ExceptionMesage(ErrorEx.NoDat);
        }

        /// <summary>
        ///     Overriding an elementof ID-Valuecollection
        /// </summary>
        /// <param name="value"></param>
        public void AddID(double value)
        {
            try // New ID and double value to the collection
            {
                _iDic.Add(_newIdS + _newIdN, value);
            }
            catch (ArgumentException)
            {
                _iDic.Remove(_newIdS + _newIdN);
                _iDic.Add(_newIdS + _newIdN, value);
            }
        }

        /// <summary>
        ///     Counting the open and close parentseses
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ParentsesesCount(string value)
        {
            _stackBrackets.Clear();
            _subStrings.Clear();
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == Templates.Separators[1]) _stackBrackets.Push(i);
                if (value[i] != Templates.Separators[2]) continue;
                var j = _stackBrackets.Pop();
                _subStrings.Add(j, i);
            }
            return 0 == _stackBrackets.Count;
        }

        /// <summary>
        ///     Reduction of the admissible pairs of arithmetic operations in a row
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string LegalTwinsCorrection(string value)
        {
            var s = "(" + value + ")";
            _iDic.Add("0", 0.0);
            _iDic.Add("-1", -1.0);
            s = s.Replace(Templates.Space, Templates.Empty);
            s = s.Replace(Templates.Coma, Templates.Line);
            s = s.Replace(Templates.Dot, Templates.Coma);
            for (var i = 0; i < _lTwins.Length/2; i++)
            {
                s = s.Replace(_lTwins[i, 0], _lTwins[i, 1]);
            }
            foreach (var t in _functionsID)
            {
                try
                {
                    _iDic.Add(t, 1.0);
                }
                catch (ArgumentException)
                {
                    throw new ExceptionMesage(ErrorEx.IllegalTwins);
                }
            }
            return s;
        }

        /// <summary>
        ///     Replacement - delete of a given char in a private collection
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="s"></param>
        /// <param name="sps"></param>
        public void CharsReplace(char[] ss, ref string s, string sps = " ")
        {
            foreach (var c in ss)
            {
                for (var i = 0; i < s.Length; i++)
                {
                    if (c != s[i]) continue;
                    s = s.Remove(i, 1);
                    s = s.Insert(i, sps);
                }
            }
            s = s.Replace(Templates.Space, Templates.Empty);
        }

        /// <summary>
        ///     Search for signs of arithmetic operations in a row
        /// </summary>
        /// <param name="s"></param>
        /// <param name="begin"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public int SignFinder(string s, int begin, char c = '_')
        {
            var result = new int[_cc1.Length];
            var so = s;
            var rez = so.Length;
            if (c != '_') return so.IndexOf(c, begin);
            for (var i = 0; i < _cc1.Length; i++)
                result[i] = so.IndexOf(_cc1[i], begin) >= 0 ? so.IndexOf(_cc1[i], begin) : so.Length;
            return result.Concat(new[] {rez}).Min();
        }

        /// <summary>
        ///     execution of arithmetic operations
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public double ArithmeticExpression(int i)
        {
            switch (i)
            {
                case 0:
                    return _af.Exponentiation();
                case 1:
                    return _af.Division();
                case 2:
                    return _af.Multiplication();
                case 3:
                    return _af.Subtraction();
                case 4:
                    return _af.Addition();
                default:
                    throw new ExceptionMesage(ErrorEx.Fatal);
            }
        }

        /// <summary>
        ///     Selection of calculated function
        /// </summary>
        /// <param name="funcID"></param>
        /// <returns></returns>
        public double Functions(string funcID)
        {
            _af.ParametersPush(_iDic[funcID]);
            return _af.FunctionValue(funcID);
        }

        #endregion
    }
}
