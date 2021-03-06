﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SSSGroup.Utilites.FormulaProcessor
{
    public enum TokenType { Number, Variable, Parenthesis, Operator, Comma, Function, WhiteSpace, DoubleOperator, Other }
    
    public class Parser
    {
        public Dictionary<string, Func<string, string>> FunctionsDelegates;

        public Parser()
        {
            FunctionsDelegates = new Dictionary<string, Func<string, string>>();
            foreach (var fun in Functions.MathFunctions)
            {
                FunctionsDelegates.Add(fun.Key, fun.Value);
            }
        }
        public Parser(Dictionary<string, Func<string, string>> customFunctions)
        {
            FunctionsDelegates = new Dictionary<string, Func<string, string>>();
            foreach (var fun in Functions.MathFunctions)
            {
                FunctionsDelegates.Add(fun.Key, fun.Value);
            }
            foreach (var fun in customFunctions)
            {
                FunctionsDelegates.Add(fun.Key, fun.Value);
            }
        }
        public string Process(string formula)
        {
            using (var reader = new StringReader(formula))
            {
                return Calculator.Evaluate(Sort(Tokenize(reader).ToList()));
            }
        }
        private bool CompareOperators(Operator op1, Operator op2)
        {
            return op1.RightAssociative ? op1.Precedence < op2.Precedence : op1.Precedence <= op2.Precedence;
        }
        private bool CompareOperators(string op1, string op2) => CompareOperators(Coll.Operators[op1], Coll.Operators[op2]);
        private TokenType DetermineType(char cur, char next = ' ')
        {
            var res = TokenType.Other;
            {
                if (char.IsLetter(cur)|| cur == '@' || cur == '\\')
                    return TokenType.Function;
                if (cur == '"' || char.IsDigit(cur))
                    return TokenType.Variable;
                if (char.IsWhiteSpace(cur))
                    return TokenType.WhiteSpace;
                switch (cur)
                {
                    case ',':
                        return TokenType.Comma;
                    case '.':
                        return TokenType.Variable;
                    case '(':
                    case ')':
                        return TokenType.Parenthesis;
                }
                res = GetOperator(cur, next, res);
            }
            //if (res == TokenType.Other)
                //throw new Exception("Wrong character");
            return res;
        }       
        public IEnumerable<Token> Tokenize(TextReader reader)
        {
            var token = new StringBuilder();
            var readingString = false;
            var readingFunction = false;
            //var passedOpeningP = false;
            int passedPar = 0;
            // var readingSmartParameter = false;
            int curr;
            while ((curr = reader.Read()) != -1)
            {
                var ch = (char)curr;
                token.Append(ch);//read current and next chars
                var next = reader.Peek();
                var nextType = next != -1 ? DetermineType((char)next) : TokenType.WhiteSpace;
                var currType = DetermineType(ch, (char)next);
                if (readingFunction)//Array of chars that represents function with parenthesis
                {
                    if (ch == '(' ) //passed opening parenthesis
                    {
                        passedPar++;
                    }
                    if (ch == ')') //passed closing parenthesis
                    {
                        passedPar--;
                    }
                    //TODO: Support Nested parenthesis and other functions inside a function
                    if (ch == ')' && 0==passedPar) //finished readign function argument with parenthesis
                    {
                        yield return new Token(TokenType.Function, token.ToString().Trim());
                        token.Clear();
                        readingFunction = false;
                    }
                    else if((TokenType.Operator == nextType || TokenType.DoubleOperator == nextType || next == ')') && 0== passedPar) //Function without parenthesis adn/or with closing parenthesis
                    {
                        yield return new Token(TokenType.Function, token.ToString().Trim());
                        token.Clear();
                        readingFunction = false;
                    }
                    continue;
                }
                if (IsString(ref readingString, ch))//Array of chars that represents string
                    continue;

                switch (currType)
                {
                    case TokenType.WhiteSpace:
                        continue;
                    case TokenType.DoubleOperator:
                        token.Append((char)reader.Read());
                        yield return new Token(TokenType.Operator, token.ToString().Trim());
                        token.Clear();
                        continue;
                }
               

                if (currType == nextType && currType != TokenType.Parenthesis && curr != '@') continue;
                if (next == '(' && currType != TokenType.Operator && currType != TokenType.Parenthesis || curr=='@')
                {
                    //We are going to 'collect' the entire string
                    readingFunction = true;
                    continue;
                }
                if (currType == TokenType.Function) continue;
                yield return new Token(currType, token.ToString().Trim());
                token.Clear();
            }
            if(readingFunction)
                yield return new Token(TokenType.Function, token.ToString().Trim());
        }      
        public IEnumerable<Token> Sort(IEnumerable<Token> tokens)
        {
            var stack = new Stack<Token>();
            foreach (var tok in tokens)
            {
                switch (tok.Type)
                {
                    case TokenType.Function:
                        {
                            if (tok.Value.StartsWith("@"))
                            {
                                yield return new Token(TokenType.Variable, (RecursivelyCallCalculations(tok.Value))); //call recursively all nested references/functions
                            }
                            else
                            {
                                var fName = tok.Value.Remove(tok.Value.IndexOf("(", StringComparison.Ordinal));
                                var fArg = tok.Value.Remove(0, tok.Value.IndexOf("(", StringComparison.Ordinal) + 1);
                                fArg = fArg.Remove(fArg.LastIndexOf(")", StringComparison.Ordinal));
                                if (!FunctionsDelegates.ContainsKey(fName))
                                    throw new Exception($"Function '{fName}' is not defined.");
                                yield return new Token(TokenType.Variable, FunctionsDelegates[fName](RecursivelyCallCalculations(fArg))); //call recursively all nested references/functions
                            }
                            
                        }
                        break;
                    case TokenType.Variable:
                        yield return tok;
                        break;                    
                    case TokenType.Comma:
                        while (stack.Peek().Value != "(")
                            yield return stack.Pop();
                        break;
                    case TokenType.Operator:
                        while (stack.Any() && stack.Peek().Type == TokenType.Operator && CompareOperators(tok.Value, stack.Peek().Value))
                            yield return stack.Pop();
                        stack.Push(tok);
                        break;
                    case TokenType.Parenthesis:
                        if (tok.Value == "(")
                            stack.Push(tok);
                        else
                        {
                            while (stack.Peek().Value != "(")
                                yield return stack.Pop();
                            stack.Pop();
                        }
                        break;
                    default:
                        throw new Exception("Wrong token");
                }
            }
            while (stack.Any())
            {
                var token = stack.Pop();
                if (token.Type == TokenType.Parenthesis)
                    throw new Exception("Mismatched parentheses");                                               
                yield return token;
            }
        }
        #region Service
        private static TokenType GetOperator(char cur, char next, TokenType res)
        {
            if (Coll.Operators.ContainsKey(Convert.ToString(cur)))
                res = TokenType.Operator;
            if (next == ' ') return res;
            if (Coll.Operators.ContainsKey(Convert.ToString(cur.ToString() + next)))
                res = TokenType.DoubleOperator;
            return res;
        }
        private bool IsString(ref bool readingString, char ch)
        {
            if (ch == '"')
            {
                readingString = !readingString;
            }
            return readingString;
        }
        private string RecursivelyCallCalculations(string param)
        {
            //Looks lile this neds to be made each specific function responsibility.
            //Going to check only for smart parameters:
            return FunctionsDelegates.ContainsKey("smartParameter") ? FunctionsDelegates["smartParameter"](param) : param;
            /*
            var nestedCall = false;
            //TODO Add nested parenthesis
            // ReSharper disable once UnusedVariable
            foreach (var fName in FunctionsDelegates.Where(fName => param.Contains(fName.Key)))
            {
                nestedCall = true;
            }
            var cc = Templates.Operators.ToCharArray();
            if (param.IndexOfAny(cc) >= 0)
            {
                nestedCall = true;
            }
            if (!nestedCall) return param;
            var parser = new Parser {FunctionsDelegates = FunctionsDelegates};
            using (var reader = new StringReader(param))
            {
                var sorted = parser.Sort(parser.Tokenize(reader).ToList());
                return Calculator.Evaluate(sorted);
            }*/
        }

        #endregion
    }
}
