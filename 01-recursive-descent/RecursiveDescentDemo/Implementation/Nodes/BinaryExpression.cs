using System;

namespace RecursiveDescentDemo.Implementation.Nodes
{
    public class BinaryExpression: Expression
    {

        public Expression LeftSubexpression { get; }

        public BinaryOperator Operator { get; }

        public Expression RightSubexpression { get; }

        public BinaryExpression(Expression left, BinaryOperator op, Expression right)
        {

            if (left == null)
                throw new ArgumentNullException(nameof(left));

            if (op == null)
                throw new ArgumentNullException(nameof(op));

            if (right == null)
                throw new ArgumentNullException(nameof(right));

            this.LeftSubexpression = left;
            this.Operator = op;
            this.RightSubexpression = right;

        }
    }
}
