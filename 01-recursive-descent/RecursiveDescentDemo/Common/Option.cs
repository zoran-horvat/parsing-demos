using System.Collections;
using System.Collections.Generic;

namespace RecursiveDescentDemo.Common
{
    public class Option<T>: IEnumerable<T>
    {
        private T[] Data { get; }

        private Option(T[] data)
        {
            this.Data = data;
        }

        public static Option<T> None()
        {
            return new Option<T>(new T[0]);
        }

        public static Option<T> Some(T value)
        {
            return new Option<T>(new[] { value });
        }

        public static Option<T> AsOption(object obj)
        {

            if (obj != null && typeof(T).IsAssignableFrom(obj.GetType()))
                return Option<T>.Some((T)obj);

            return Option<T>.None();

        }  

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
