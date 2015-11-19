namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class UnexpectedInput: PositionedToken
    {

        public UnexpectedInput(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "<<unexpected input>>";
        }
    }
}
