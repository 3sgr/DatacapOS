namespace SSSGroup.Utilites.FormulaProcessor
{
    public class Operator
    {
        public string Name { get; set; }
        public int Precedence { get; set; }
        public bool RightAssociative { get; set; }
    }
}