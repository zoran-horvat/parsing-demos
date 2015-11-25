using System;
using System.Diagnostics.Contracts;

namespace Interfaces
{
    public class InvisibleStringClassToken : IToken
    {
        private string Class { get; }
        public string Representation => string.Empty;

        public InvisibleStringClassToken(string className)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(className), "Token class name must be non-empty.");
            this.Class = className;
        }

        public override string ToString()
        {
            return this.Class;
        }
    }
}
