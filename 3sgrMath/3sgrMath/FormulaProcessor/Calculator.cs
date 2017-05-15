using System.Collections.Generic;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public static class Calculator
    {
        public static string Evaluate(IEnumerable<Token> expression)
        {
            string smallRes;
            var stack = new Stack<Token>();
            foreach (var token in expression)
            {
                if(token.Type==TokenType.Variable)
                    stack.Push(token);
                if (token.Type != TokenType.Operator) continue;
                var ft = new FormatTranslator();
                var op2 = stack.Pop().ToString();
                if (token.Value != "!")
                {
                    var op1 = stack.Pop().ToString();
                    smallRes = BaseMath.DoMath(ft.Factory(op1), ft.Factory(op2), token.Value);
                }
                else
                {
                   smallRes = BaseMath.DoMath(ft.Factory(op2), token.Value);
                }
                var v = new Token(TokenType.Variable, smallRes);
                stack.Push(v);
            }
            return stack.Pop().ToString();
        }
    }
}
