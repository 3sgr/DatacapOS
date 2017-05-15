using System.Collections.Generic;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public static  class Coll
    {
        public static readonly IDictionary<string, Operator> Operators = new Dictionary<string, Operator>
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
            ["="] = new Operator { Name = "==", Precedence = 3, RightAssociative = true }, //required to parse smart parameters
            ["!="] = new Operator { Name = "!=", Precedence = 3, RightAssociative = true },
            ["&"] = new Operator { Name = "&", Precedence = 4 },
            ["|"] = new Operator { Name = "|", Precedence = 3 },
            ["!"] = new Operator { Name = "!", Precedence = 4, RightAssociative = true }
        };
    }
}