// MvxEnumerableExtensions.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.Collections;
using System.Collections.Generic;

namespace Cirrious.MvvmCross.Binding.ExtensionMethods
{
    public static class MvxEnumerableExtensions
    {
        public static int Count(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return 0;

            var itemsList = enumerable as ICollection;
            if (itemsList != null)
            {
                return itemsList.Count;
            }

            var enumerator = enumerable.GetEnumerator();
            var count = 0;
            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        public static int GetPosition(this IEnumerable items, object item)
        {
            if (items == null)
            {
                return -1;
            }

            var itemsList = items as IList;
            if (itemsList != null)
            {
                {
                    return itemsList.IndexOf(item);
                }
            }

            var enumerator = items.GetEnumerator();
            for (var i = 0;; i++)
            {
                if (!enumerator.MoveNext())
                {
                    return -1;
                }

                if (enumerator.Current == item)
                {
                    return i;
                }
            }
        }

        public static System.Object ElementAt(this IEnumerable items, int position)
        {
            if (items == null)
                return null;

            var itemsList = items as IList;
            if (itemsList != null)
            {
                return itemsList[position];
            }

            var enumerator = items.GetEnumerator();
            for (var i = 0; i <= position; i++)
            {
                enumerator.MoveNext();
            }

            return enumerator.Current;
        }

		/// <summary>
		/// Creates an IEnumerable from any single item.
		/// </summary>
		public static IEnumerable<T> ToEnumerable<T>(this T item) {
			yield return item;
		}
    }
}