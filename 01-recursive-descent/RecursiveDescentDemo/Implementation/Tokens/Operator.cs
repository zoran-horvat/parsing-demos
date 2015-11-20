namespace RecursiveDescentDemo.Implementation.Tokens
{
    public class Operator: RepresentableToken<char>
    {
        private Operator(int pos, char representation) : base(pos, representation)
        {
        }

        public static Operator BinaryPlus(int pos)
        {
            return new Operator(pos, '+');
        }

        public static Operator BinaryMinus(int pos)
        {
            return new Operator(pos, '-');
        }

        public static Operator Multiplication(int pos)
        {
            return new Operator(pos, '*');
        }

        public static Operator Division(int pos)
        {
            return new Operator(pos, '/');
        }
    }
}
