using System;
using System.Collections.Generic;
using System.Linq;
using RecursiveDescentDemo.Common;
using RecursiveDescentDemo.Implementation.Nodes;
using RecursiveDescentDemo.Implementation.Tokens;
using RecursiveDescentDemo.Interfaces;

namespace RecursiveDescentDemo.Implementation
{
    public class RecursiveDescentParser: IParser
    {

        private IEnumerator<IToken> TokenEnumerator { get; set; }
        private bool InputReady { get; set; }

        private IToken CurrentToken
        {
            get
            {
                if (!this.InputReady)
                    throw new InvalidOperationException();
                return this.TokenEnumerator.Current;
            }
        }

        private int CurrentInputPosition => this.CurrentToken.InputPosition;

        private void FetchNextToken()
        {
            this.InputReady = this.TokenEnumerator.MoveNext();
        }

        public Option<IAbstractSyntaxTree> TryParse(IEnumerable<IToken> tokens)
        {

            this.TokenEnumerator = tokens.GetEnumerator();
            this.FetchNextToken();

            return TryParseEntireExpression();

        }

        private Option<IAbstractSyntaxTree> TryParseEntireExpression()
        {

            Option<Expression> expression = this.TryParseExpression();
            if (!expression.Any())
                return this.Fail<IAbstractSyntaxTree>();

            this.FetchNextToken();
            if (!this.TryParseEndOfFile())
                return this.Fail<IAbstractSyntaxTree>("Unexpected character when expecting end of file.");

            return Option<IAbstractSyntaxTree>.Some(expression.Single());

        }

        private Option<Expression> TryParseExpression()
        {

            if (IsInputOfType<Digit>())
                return Option<Expression>.Some(this.ParseConstantExpression(this.CurrentToken as Digit));

            if (!IsInputOfType<OpenParenthesis>())
                return this.Fail<Expression>("Expected constant or open parenthesis.");

            this.FetchNextToken();
            Option<Expression> leftSubexpression = this.TryParseExpression();
            if (!leftSubexpression.Any())
                return this.Fail<Expression>();

            this.FetchNextToken();
            Option<BinaryOperator> op = this.TryParseOperator();
            if (!op.Any())
                return this.Fail<Expression>();

            this.FetchNextToken();
            Option<Expression> rightSubexpression = this.TryParseExpression();
            if (!rightSubexpression.Any())
                return this.Fail<Expression>();

            this.FetchNextToken();
            if (!this.IsInputOfType<ClosedParenthesis>())
                return this.Fail<Expression>("Expected closing parenthesis.");

            Expression expression = new BinaryExpression(leftSubexpression.Single(), op.Single(), rightSubexpression.Single());

            return Option<Expression>.Some(expression);

        }

        private Option<BinaryOperator> TryParseOperator()
        {

            if (!this.IsInputOfType<Operator>())
                return this.Fail<BinaryOperator>("Expected operator.");

            return Option<BinaryOperator>.Some(new BinaryOperator(this.CurrentToken as Operator));

        }

        private Expression ParseConstantExpression(Digit digit)
        {
            return new ConstantExpression(digit.Value);
        }

        private bool IsInputOfType<T>()
        {
            return this.InputReady && this.CurrentToken is T;
        }

        private bool TryParseEndOfFile()
        {
            return this.IsInputOfType<EndOfFile>();
        }

        private Option<T> Fail<T>()
        {
            return Option<T>.None();
        } 
        private Option<T> Fail<T>(string errorMessage)
        {
            Console.WriteLine(new string(' ', this.CurrentInputPosition) + "^");
            Console.WriteLine(errorMessage);
            return this.Fail<T>();
        } 

    }
}
