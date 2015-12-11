using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common
{
    public class Option<T>: IEnumerable<T>
    {
        private T[] Data { get; }

        private Option(T[] data)
        {
            this.Data = data;
        }

        public static Option<T> Some(T value)
        {
            Contract.Ensures(Contract.Result<Option<T>>() != null);
            return new Option<T>(new T[] { value });
        }

        public static Option<T> None()
        {
            Contract.Ensures(Contract.Result<Option<T>>() != null);
            return new Option<T>(new T[0]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator<T>>() != null);
            return ((IEnumerable<T>)this.Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return this.GetEnumerator();
        }

        private bool IsNone => this.Data.Length == 0;

        public override string ToString()
        {

            Contract.Ensures(Contract.Result<string>() != null);

            if (this.IsNone)
                return "None";

            if (object.ReferenceEquals(null, this.Data[0]))
                return "null";

            return this.Data[0].ToString();

        }
    }
}
