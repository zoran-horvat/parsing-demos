namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Digit: PositionedToken
    {

        public int Value { get; }

        public Digit(int pos, int value) : base(pos)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString("0");
        }
    }
}
