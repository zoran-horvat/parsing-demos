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
                        yield return Operator.BinaryPlus(pos);
                        break;
                    case '-':
                        yield return Operator.BinaryMinus(pos);
                        break;
                    case '*':
                        yield return Operator.Multiplication(pos);
                        break;
                    case '/':
                        yield return Operator.Division(pos);
                        break;
                    case '(':
                        yield return Parenthesis.Open(pos);
                        break;
                    case ')':
                        yield return Parenthesis.Closed(pos);
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
