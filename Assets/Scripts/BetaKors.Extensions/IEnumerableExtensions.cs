using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BetaKors.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T, int)> Enumerate<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((e, i) => (e, i));
        }

        // adapted from: https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        public static T[] Shuffle<T>(this IEnumerable<T> source)
        {
            var array = source.ToArray();
            var n = array.Count();

            while (n > 1)
            {
                var k = Random.Range(0, n--);
                (array[k], array[n]) = (array[n], array[k]);
            }

            return array;
        }

        // adapted from: https://stackoverflow.com/a/3173726
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default : list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static void ForEach<T>(this IEnumerable<T> source, System.Action<T> function)
        {
            foreach (var element in source)
            {
                function(element);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}
