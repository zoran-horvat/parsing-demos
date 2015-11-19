namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class EndOfFile: PositionedToken
    {

        public EndOfFile(int pos) : base(pos)
        {
        }

        public override string ToString()
        {
            return "<<eof>>";
        }
    }
}
