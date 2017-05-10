namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public struct Token
    {

        public TokenType Type { get; }
        public string Value { get; }
        public override string ToString() => $"{Value}";
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}