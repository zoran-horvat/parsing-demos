using System;
using System.Linq;
using RecursiveDescentDemo.Common;
using RecursiveDescentDemo.Implementation.Nodes;
using RecursiveDescentDemo.Interfaces;

namespace RecursiveDescentDemo.Implementation.Generators
{
    public class ExpressionValueCalculator: IGenerator
    {
        public void Process(IAbstractSyntaxTree ast)
        {
            int value = this.ValueOf(Option<Expression>.AsOption(ast));
            Console.WriteLine("Expression value is {0}", value);
        }

        private int ValueOf(Expression expression)
        {
            return this.ValueOf(Option<Expression>.Some(expression));
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

            int leftValue = this.ValueOf(left);
            int rightValue = this.ValueOf(right);

            return this.ValueOf(leftValue, op.Representation, rightValue);

        }

        private int ValueOf(int leftValue, char op, int rightValue)
        {

            if (op == '+')
                return leftValue + rightValue;

            if (op == '-')
                return leftValue - rightValue;

            if (op == '*')
                return leftValue*rightValue;

            if (op == '/')
                return leftValue/rightValue;

            return 0;

        }
    }
}
