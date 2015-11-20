namespace RecursiveDescentDemo.Implementation.Tokens
{
    public abstract class RepresentableToken<T>: PositionedToken
    {
        public T Representation { get; }

        public RepresentableToken(int pos, T representation): base(pos)
        {
            this.Representation = representation;
        }

        public override string ToString()
        {
            return this.Representation.ToString();
        }
    }
}
