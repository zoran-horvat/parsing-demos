using System;
using RecursiveDescentDemo.Implementation.Tokens;

namespace RecursiveDescentDemo.Implementation.Nodes
{
    public class BinaryOperator
    {
        public char Representation { get; }

        public BinaryOperator(Operator op)
        {

            if (op == null)
                throw new ArgumentNullException(nameof(op));

            this.Representation = op.Representation;

        }

        public override string ToString()
        {
            return this.Representation.ToString();
        }
    }
}
