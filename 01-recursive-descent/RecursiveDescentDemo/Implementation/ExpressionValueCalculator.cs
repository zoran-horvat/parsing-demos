using System;
using System.Linq;
using RecursiveDescentDemo.Common;
using RecursiveDescentDemo.Implementation.Nodes;
using RecursiveDescentDemo.Implementation.Tokens;
using RecursiveDescentDemo.Interfaces;

namespace RecursiveDescentDemo.Implementation
{
    public class ExpressionValueCalculator: IGenerator
    {
        public void Process(IAbstractSyntaxTree ast)
        {
            int value = this.ValueOf(Option<Expression>.AsOption(ast));
            Console.WriteLine("Expression value is {0}", value);
        }

        private int ValueOf(Option<Expression> expression)
        {
            return
                expression
                    .Select(expr =>
                        this.ValueOf(Option<BinaryExpression>.AsOption(expr)) +
                        this.ValueOf(Option<ConstantExpression>.AsOption(expr)))
                    .DefaultIfEmpty(0)
                    .Single();
        }

        private int ValueOf(Option<BinaryExpression> expression)
        {
            return
                expression
                    .Select(expr => this.ValueOf(expr.LeftSubexpression, expr.Operator, expr.RightSubexpression))
                    .DefaultIfEmpty(0)
                    .Single();
        }

        private int ValueOf(Option<ConstantExpression> expression)
        {
            return
                expression
                    .Select(expr => expr.Value)
                    .DefaultIfEmpty(0)
                    .Single();
        }

        private int ValueOf(Expression left, BinaryOperator op, Expression right)
        {

            int leftValue = this.ValueOf(Option<Expression>.Some(left));
            int rightValue = this.ValueOf(Option<Expression>.Some(right));

            return this.ValueOf(leftValue, op.Operator, rightValue);

        }

        private int ValueOf(int leftValue, Operator op, int rightValue)
        {

            if (op is Addition)
                return leftValue + rightValue;

            if (op is Subtraction)
                return leftValue - rightValue;

            if (op is Multiplication)
                return leftValue*rightValue;

            if (op is Division)
                return leftValue/rightValue;

            return 0;

        }
    }
}
