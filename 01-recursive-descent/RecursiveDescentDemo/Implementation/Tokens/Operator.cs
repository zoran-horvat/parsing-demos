namespace RecursiveDescentDemo.Implementation.Tokens
{
    public abstract class Operator: PositionedToken
    {
        protected Operator(int pos) : base(pos)
        {
        }
    }
}
