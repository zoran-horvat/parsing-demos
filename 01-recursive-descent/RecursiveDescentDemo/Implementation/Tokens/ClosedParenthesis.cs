namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class ClosedParenthesis: PositionedToken
    {
        public ClosedParenthesis(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return ")";
        }

    }
}
