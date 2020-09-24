using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RealMan
{
    static public class CollectionExtention
    {
        // for IEnumerable
        static public void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            for (int index = 0; index < enumerable.Count(); index++)
                action.Invoke(enumerable.ElementAt(index));
        }

        static public void Foreach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            for (int index = 0; index < enumerable.Count(); index++)
                action.Invoke(enumerable.ElementAt(index), index);
        }

        static public void Foreach<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, Action<T> action)
        {
            for (int index = 0; index < enumerable.Count(); index++)
			{
                T element = enumerable.ElementAt(index);
                if (predicate.Invoke(element) == false)
                    continue;

                action.Invoke(element);
            }
        }

        static public void Foreach<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, Action<T> action)
        {
            for (int index = 0; index < enumerable.Count(); index++)
            {
                T element = enumerable.ElementAt(index);
                if (predicate.Invoke(element) == false)
                    continue;

                action.Invoke(element);
            }
        }


        // for ICollection
        static public void Foreach<T>(this ICollection<T> collection, Action<T> action)
        {
            for (int index = 0; index < collection.Count; index++)
                action.Invoke(collection.ElementAt(index));
        }

        static public void Foreach<T>(this ICollection<T> collection, Action<T, int> action)
        {
            for (int index = 0; index < collection.Count; index++)
                action.Invoke(collection.ElementAt(index), index);
        }

        static public void Foreach<T>(this ICollection<T> collection, Predicate<T> predicate, Action<T> action)
        {
            for (int index = 0; index < collection.Count; index++)
            {
                var element = collection.ElementAt(index);
                if (predicate.Invoke(element) == false)
                    continue;

                action.Invoke(element);
            }
        }

        static public bool IsInRange(this ICollection collection, int val) => (0 <= val) && (val < collection.Count);

        static public bool IsOutOfRange(this ICollection collection, int val) => (0 > val) || (val >= collection.Count);
    }
}