namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Division: Operator
    {

        public Division(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "/";
        }
    }
}
