using System.Collections.Generic;

namespace BetaKors.Extensions
{
    public static class IListExtensions
    {
        public static T Pop<T>(this IList<T> array, int index)
        {
            var element = array[index];
            array.RemoveAt(index);
            return element;
        }

        public static T PopRandom<T>(this IList<T> array)
        {
            var element = array.RandomElement();
            array.Remove(element);
            return element;
        }
    }
}
