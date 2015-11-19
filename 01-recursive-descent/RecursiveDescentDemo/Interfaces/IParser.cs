using System.Collections.Generic;
using RecursiveDescentDemo.Common;

namespace RecursiveDescentDemo.Interfaces
{
    public interface IParser
    {
        Option<IAbstractSyntaxTree> TryParse(IEnumerable<IToken> tokens);
    }
}
