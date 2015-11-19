using RecursiveDescentDemo.Interfaces;

namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class PositionedToken: IToken
    {
        public int InputPosition { get; }

        public PositionedToken(int pos)
        {
            this.InputPosition = pos;
        }
    }
}
