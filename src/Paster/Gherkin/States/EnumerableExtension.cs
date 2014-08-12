using System;
using System.Collections.Generic;

namespace xBehave.Paster.Gherkin
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> DropLast<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return InternalDropLast(source);
        }

        private static IEnumerable<T> InternalDropLast<T>(IEnumerable<T> source)
        {
            T buffer = default(T);
            bool buffered = false;

            foreach (var x in source)
            {
                if (buffered)
                    yield return buffer;

                buffer = x;
                buffered = true;
            }
        }
    }
}