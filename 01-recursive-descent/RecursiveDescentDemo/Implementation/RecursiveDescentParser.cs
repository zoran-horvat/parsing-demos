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

            Option<Expression> constant = this.TryParseConstantExpression();
            if (constant.Any())
                return constant;

            return this.TryParseBinaryExpression();

        }

        private Option<Expression> TryParseBinaryExpression()
        {

            if (!IsInputOfType<Parenthesis, char>('('))
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
            if (!this.IsInputOfType<Parenthesis, char>(')'))
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

        private Option<Expression> TryParseConstantExpression()
        {

            if (!IsInputOfType<Digit>())
                return Option<Expression>.None();

            Digit digit = this.CurrentToken as Digit;
            Expression expression = new ConstantExpression(digit.Value);

            return Option<Expression>.Some(expression);

        }

        private bool IsInputOfType<T>()
        {
            return this.InputReady && this.CurrentToken is T;
        }

        private bool IsInputOfType<TInput, TRepresentation>(TRepresentation representation) where TInput: RepresentableToken<TRepresentation>
        {
            return this.IsInputOfType<TInput>() && this.IsRepresentation(representation);
        }

        private bool IsRepresentation<TRepresentation>(TRepresentation representation)
        {
            RepresentableToken<TRepresentation> token = this.CurrentToken as RepresentableToken<TRepresentation>;
            return token != null && token.Representation.Equals(representation);
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
