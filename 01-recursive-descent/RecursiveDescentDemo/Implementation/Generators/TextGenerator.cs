using System;
using System.Linq;
using RecursiveDescentDemo.Common;
using RecursiveDescentDemo.Implementation.Nodes;
using RecursiveDescentDemo.Interfaces;

namespace RecursiveDescentDemo.Implementation.Generators
{
    public class TextGenerator: IGenerator
    {
        public void Process(IAbstractSyntaxTree ast)
        {
            string expression = ToString(Option<Expression>.AsOption(ast));
            Console.WriteLine("Formatted expression: {0}", expression);
        }

        private string ToString(Option<Expression> expression)
        {
            return
                expression
                    .Select(expr =>
                        ToString(Option<BinaryExpression>.AsOption(expr)) +
                        ToString(Option<ConstantExpression>.AsOption(expr)))
                    .DefaultIfEmpty(string.Empty)
                    .Single();
        }

        private string ToString(Option<BinaryExpression> binaryExpression)
        {
            return
                binaryExpression
                    .Select(expr =>
                        string.Format("({0} {1} {2})",
                            this.ToString(Option<Expression>.AsOption(expr.LeftSubexpression)),
                            expr.Operator.ToString(),
                            this.ToString(Option<Expression>.AsOption(expr.RightSubexpression))))
                    .DefaultIfEmpty(string.Empty)
                    .Single();
        }

        private string ToString(Option<ConstantExpression> constant)
        {
            return 
                constant
                .Select(c => c.Value.ToString("0"))
                .DefaultIfEmpty(string.Empty)
                .Single();
        }

    }
}
