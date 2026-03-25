namespace Framework.Core.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The collection extensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Append unique non-blank values to the list.
        /// </summary>
        /// <param name="list">The list to append values to.</param>
        /// <param name="valuesToAppend">The values to append.</param>
        /// <exception cref="ArgumentNullException">The list to accept the values must not be null.</exception>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<string> AppendUniqueValues(this List<string> list, IEnumerable<string> valuesToAppend)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (valuesToAppend != null)
            {
                list.AddRange(valuesToAppend.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct());
            }

            return list;
        }

        /// <summary>
        /// Verify that two collections are equivalent: they have the same elements in the same quantity, but in any order. 
        /// Elements are equal if their values are equal, not if they refer to the same object
        /// </summary>
        /// <param name="firstCollection">
        /// The first collection.
        /// </param>
        /// <param name="secondCollection">
        /// The second collection.
        /// </param>
        /// <typeparam name="T"> Type of element in collection
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>. True if collections are equivalent
        /// </returns>
        public static bool CollectionEquals<T>(this ICollection<T> firstCollection, ICollection<T> secondCollection)
        {
            if (firstCollection == null)
            {
                return secondCollection == null;
            }

            if (secondCollection == null)
            {
                return false;
            }

            if (object.ReferenceEquals(firstCollection, secondCollection))
            {
                return true;
            }

            if (firstCollection.Count != secondCollection.Count)
            {
                return false;
            }

            if (firstCollection.Count == 0 && secondCollection.Count == 0)
            {
                return true;
            }

            return !CollectionExtensions.HaveMismatchedElement(firstCollection, secondCollection);
        }

        /// <summary>
        /// The deep copy for Value Type collections. Return copy of Collection
        /// </summary>
        /// <param name="originalCollection">
        /// The original collection.
        /// </param>
        /// <param name="startingIndex">
        /// The starting index.
        /// </param>
        /// <typeparam name="T"> Type
        /// </typeparam>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public static List<T> CopyCollection<T>(this IList<T> originalCollection, int startingIndex = 0)
        {
            T[] arrayForCopy = new T[originalCollection.Count];
            originalCollection.CopyTo(arrayForCopy, startingIndex);
            return arrayForCopy.ToList();
        }

        /// <summary>
        /// Verify that two nested IEnumerable are equal
        /// </summary>
        /// <param name="first">
        /// The first IEnumerable.
        /// </param>
        /// <param name="second">
        /// The second IEnumerable.
        /// </param>
        /// <typeparam name="T"> Type of element in nested IEnumerable
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>. True if nested IEnumerable are equal
        /// </returns>
        public static bool NestedSequenceEqual<T>(
            this IEnumerable<IEnumerable<T>> first,
            IEnumerable<IEnumerable<T>> second)
        {
            return first.Zip(second, (f, s) => f.SequenceEqual(s)).All(b => b);
        }

        /// <summary>
        /// The union except intersection.
        /// Example: the first collection: {"a", "b", "c"}, the second collection: {"b", "c", "d"} - the result collection will be {"a", "d"}
        /// </summary>
        /// <param name="first">The first collection.</param>
        /// <param name="second">The second collection.</param> 
        /// <typeparam name="T">The type of items in collections.</typeparam>
        /// <returns>The result collection.</returns>
        public static IEnumerable<T> UnionExceptIntersection<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            T[] enumerable1 = first as T[] ?? first.ToArray();
            T[] enumerable2 = second as T[] ?? second.ToArray();

            return enumerable2.Union(enumerable1).Except(enumerable2.Intersect(enumerable1));
        }

        private static Dictionary<T, int> GetElementCounts<T>(IEnumerable<T> enumerable, out int nullCount)
        {
            var dictionary = new Dictionary<T, int>();
            nullCount = 0;

            foreach (T element in enumerable)
            {
                if (element == null)
                {
                    nullCount++;
                }
                else
                {
                    int num;
                    dictionary.TryGetValue(element, out num);
                    num++;
                    dictionary[element] = num;
                }
            }

            return dictionary;
        }

        private static bool HaveMismatchedElement<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            int firstNullCount;
            int secondNullCount;

            Dictionary<T, int> firstElementCounts = CollectionExtensions.GetElementCounts(first, out firstNullCount);
            Dictionary<T, int> secondElementCounts = CollectionExtensions.GetElementCounts(second, out secondNullCount);

            if (firstNullCount != secondNullCount || firstElementCounts.Count != secondElementCounts.Count)
            {
                return true;
            }

            foreach (KeyValuePair<T, int> kvp in firstElementCounts)
            {
                int firstElementCount = kvp.Value;
                int secondElementCount;
                secondElementCounts.TryGetValue(kvp.Key, out secondElementCount);

                if (firstElementCount != secondElementCount)
                {
                    return true;
                }
            }

            return false;
        }
    }
}