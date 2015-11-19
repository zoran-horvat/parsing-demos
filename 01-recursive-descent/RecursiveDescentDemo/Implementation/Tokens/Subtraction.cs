namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Subtraction: Operator
    {

        public Subtraction(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
