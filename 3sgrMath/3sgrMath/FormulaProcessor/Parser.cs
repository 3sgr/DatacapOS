using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public enum TokenType { Number, Variable, Parenthesis, Operator, Comma, WhiteSpace, DoubleOperator, Invalid }
    public class Parser
    {
        private readonly IDictionary<string, Operator> _operators = new Dictionary<string, Operator>
        {
            ["+"] = new Operator { Name = "+", Precedence = 5 },
            ["-"] = new Operator { Name = "-", Precedence = 5 },
            ["*"] = new Operator { Name = "*", Precedence = 7 },
            ["/"] = new Operator { Name = "/", Precedence = 7 },
            ["^"] = new Operator { Name = "^", Precedence = 8, RightAssociative = true },
            [">"] = new Operator { Name = ">", Precedence = 3, RightAssociative = true },
            ["<"] = new Operator { Name = "<", Precedence = 3, RightAssociative = true },
            ["<="] = new Operator { Name = "<=", Precedence = 3, RightAssociative = true },
            ["=<"] = new Operator { Name = "=<", Precedence = 3, RightAssociative = true },
            [">="] = new Operator { Name = ">=", Precedence = 3, RightAssociative = true },
            ["=>"] = new Operator { Name = "=>", Precedence = 3, RightAssociative = true },
            ["=="] = new Operator { Name = "==", Precedence = 3, RightAssociative = true },
            ["!="] = new Operator { Name = "!=", Precedence = 3, RightAssociative = true },
            ["&"] = new Operator { Name = "&", Precedence = 4},
            ["|"] = new Operator { Name = "|", Precedence = 3},
            ["!"] = new Operator { Name = "!", Precedence = 4, RightAssociative = true }
        };
        private bool CompareOperators(Operator op1, Operator op2)
        {
            return op1.RightAssociative ? op1.Precedence < op2.Precedence : op1.Precedence <= op2.Precedence;
        }
        private bool CompareOperators(string op1, string op2) => CompareOperators(_operators[op1], _operators[op2]);
        private TokenType DetermineType(char cur, char next = ' ')
        {
            var res = TokenType.Invalid;
            {
                if (char.IsLetter(cur) || cur == '"' || char.IsDigit(cur))
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
                if (_operators.ContainsKey(Convert.ToString(cur)))
                    res = TokenType.Operator;
                if (next != ' ')
                    if (_operators.ContainsKey(Convert.ToString(cur.ToString() + next)))
                        res = TokenType.DoubleOperator;
            }
            if (res == TokenType.Invalid)
                throw new Exception("Wrong character");
            return res;
        }
        public IEnumerable<Token> Tokenize(TextReader reader)
        {
            var token = new StringBuilder();
            var readingString = false;
            int curr;
            while ((curr = reader.Read()) != -1)
            {
                var ch = (char)curr;
                token.Append(ch);
                if (!readingString && ch == '"')
                {
                    readingString = true;
                }
                else if (readingString && ch == '"')
                {
                    readingString = false;
                }
                if(readingString)
                    continue;
                    
                var next = reader.Peek();
                var currType = DetermineType(ch, (char)next);
                if (currType == TokenType.WhiteSpace)
                {
                    continue;
                }
                if (currType == TokenType.DoubleOperator)
                {
                    token.Append((char) reader.Read());
                    yield return new Token(TokenType.Operator, token.ToString().Trim());
                    token.Clear();
                    continue;
                }
                var nextType = next != -1 && (char)next!='=' ? DetermineType((char)next) : TokenType.WhiteSpace;
                if (currType == nextType && currType!=TokenType.Parenthesis) continue;
                yield return new Token(currType, token.ToString().Trim());
                token.Clear();
            }
        }
        public IEnumerable<Token> Sort(IEnumerable<Token> tokens)
        {
            var stack = new Stack<Token>();
            foreach (var tok in tokens)
            {
                switch (tok.Type)
                {
                    case TokenType.Number:
                    case TokenType.Variable:
                        yield return tok;
                        break;
                    /*case TokenType.Function:
                        stack.Push(tok);
                        break;*/
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
    }
}
