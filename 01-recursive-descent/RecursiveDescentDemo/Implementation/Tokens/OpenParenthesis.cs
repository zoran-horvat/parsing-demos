namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class OpenParenthesis: PositionedToken
    {

        public OpenParenthesis(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "(";
        }
    }
}
