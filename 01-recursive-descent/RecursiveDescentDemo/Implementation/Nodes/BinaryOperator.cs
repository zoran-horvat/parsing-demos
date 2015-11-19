using System;
using RecursiveDescentDemo.Implementation.Tokens;

namespace RecursiveDescentDemo.Implementation.Nodes
{
    public class BinaryOperator
    {
        public Operator Operator { get; }

        public BinaryOperator(Operator op)
        {

            if (op == null)
                throw new ArgumentNullException(nameof(op));

            this.Operator = op;

        }

        public override string ToString()
        {
            return this.Operator.ToString();
        }
    }
}
