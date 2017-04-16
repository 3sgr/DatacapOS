using System;
using System.Collections.Generic;
using Convertion.Properties;

namespace Convertion
{
    #region ErrorException
    public class ErrorMesage : Exception
    {
        public ErrorMesage() { }
        public ErrorMesage(string message) : base(message) { }
    }
    #endregion


    /// <summary>
    /// Calculating the values of operations and  functions
    /// </summary>
    class ActionsAndFunctions 
    {
      private readonly Stack<double> _parameters = new Stack<double>(0);       // Parameters Stack
        
        #region Service
        public void ParametersPush(double dig)
        { _parameters.Push(dig); }
        public double ParametersPop()
        { return _parameters.Pop(); }
        public void ParametersClear()
        { _parameters.Clear(); }
        #endregion

        #region Actions
        public double Exponentiation(int n=2)
        {
          double rez;
          if (n != 2) throw new ErrorMesage (ErrorAr._1);
          double pov= ParametersPop(), num = ParametersPop();
          if (Math.Abs(pov) > 0.0001) { try { rez = Math.Exp(pov*Math.Log(num )); } catch (OverflowException) { throw new ErrorMesage(ErrorAr._11); }}
          else rez = 1;
          return rez;
        }
        public double Division(int n = 2)
        {
          double rez;
          if (n != 2) throw new ErrorMesage(ErrorAr._2);
          double den = ParametersPop(), num = ParametersPop();
          try { rez = num/den; } catch (DivideByZeroException) { throw new ErrorMesage(ErrorAr._21); }
          return rez;
        }
        public double Multiplication(int n = 2)
        {
         if (n != 2) throw new ErrorMesage(ErrorAr._3);
         double rez;
         try { rez = (ParametersPop() * ParametersPop()) ;} catch (OverflowException) { throw new ErrorMesage(ErrorAr._31); }
         return rez;
        }
        public double Subtraction (int n = 2)
        {
         if (n != 2) throw new ErrorMesage(ErrorAr._4);
         double rez;
         try { rez = -(ParametersPop() - ParametersPop());} catch (OverflowException) { throw new ErrorMesage(ErrorAr._41); }
         return rez;
        }
        public double Addition(int n = 2)
        {
         if (n != 2) throw new ErrorMesage(ErrorAr._5);
         double rez;
         try { rez = (ParametersPop() + ParametersPop()); } catch (OverflowException) { throw new ErrorMesage(ErrorAr._51); }
         return rez;
        }
        #endregion

        /// <summary>
        /// //"Bypass" always returns the value of the argument
        /// </summary>
        /// <param name="funcID"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public double  FunctionValue(string funcID, int n = 1)   //"Bypass" always returns the value of the argument
        {
          double rez;
          try { ParametersPop(); } catch (OverflowException) { throw new ErrorMesage(ErrorAr._6); }
          try { rez = ParametersPop(); } catch (OverflowException) { throw new ErrorMesage(ErrorAr._6); }
            switch (funcID)
            {
                case "sqrt": rez = Math.Sqrt(rez); break;
                case "ln"  : rez = Math.Log(rez) ; break;
                case "exp" : rez = Math.Exp(rez) ; break;
                case "abs" : rez = Math.Abs(rez ); break;
                case "sin" : rez = Math.Sin(rez) ; break;
                case "cos" : rez = Math.Cos(rez) ; break;
                case "asin": rez = Math.Asin(rez); break;
                case "acos": rez = Math.Acos(rez); break;
                default    : throw new ErrorMesage(ErrorAr._66);
            }
            return rez;  
        }
        // The end of the Class
    }
}
