namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Multiplication: Operator
    {

        public Multiplication(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
