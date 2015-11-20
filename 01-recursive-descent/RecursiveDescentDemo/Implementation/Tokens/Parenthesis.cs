namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Parenthesis: RepresentableToken<char>
    {

        private Parenthesis(int pos, char representation) : base(pos, representation)
        {
        }

        public static Parenthesis Open(int pos)
        {
            return new Parenthesis(pos, '(');
        }

        public static Parenthesis Closed(int pos)
        {
            return new Parenthesis(pos, ')');
        }
    }
}
