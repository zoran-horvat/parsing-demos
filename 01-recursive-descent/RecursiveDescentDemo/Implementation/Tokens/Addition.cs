namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Addition: Operator
    {
        public Addition(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "+";
        }

    }
}
