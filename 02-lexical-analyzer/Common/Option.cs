using System.Collections;
using System.Collections.Generic;

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
            return new Option<T>(new T[] { value });
        }

        public static Option<T> None()
        {
            return new Option<T>(new T[0]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool IsNone => this.Data.Length == 0;

        public override string ToString()
        {

            if (this.IsNone)
                return "None";

            if (object.ReferenceEquals(null, this.Data[0]))
                return "null";

            return this.Data[0].ToString();

        }
    }
}
