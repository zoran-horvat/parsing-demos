using System;
using System.Collections.Generic;
using RecursiveDescentDemo.Implementation.Tokens;
using RecursiveDescentDemo.Interfaces;

namespace RecursiveDescentDemo.Implementation
{
    public class SimpleLexicalAnalyzer: ILexicalAnalyzer
    {
        public IEnumerable<IToken> Analyze(IEnumerable<char> input)
        {

            int pos = 0;

            foreach (char c in input)
            {
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        yield return new Digit(pos, c - '0');
                        break;
                    case '+':
                        yield return new Addition(pos);
                        break;
                    case '-':
                        yield return new Subtraction(pos);
                        break;
                    case '*':
                        yield return new Multiplication(pos);
                        break;
                    case '/':
                        yield return new Division(pos);
                        break;
                    case '(':
                        yield return new OpenParenthesis(pos);
                        break;
                    case ')':
                        yield return new ClosedParenthesis(pos);
                        break;
                    case ' ':
                    case '\t':
                        break;
                    default:
                        yield return new UnexpectedInput(pos);
                        yield break;
                }

                pos++;

            }

            yield return new EndOfFile(pos);

        }
    }
}
