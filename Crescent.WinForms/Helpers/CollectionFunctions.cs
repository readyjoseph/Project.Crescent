using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Crescent.WinForms.Helpers
{
    public static class CollectionFunctions
    {
        public static string Concatenate(this IEnumerable list, string delimiter, bool excludeEmpty = false)
        {
            var s = string.Empty;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var text = item?.ToString();
                    if (!excludeEmpty
                        || string.IsNullOrWhiteSpace(text))
                        if (s != string.Empty)
                            s += $"{delimiter}{text}";
                        else
                            s += text;
                }
            }
            return s;
        }
        public static string Concatenate(this Array array, string delimiter, bool excludeEmpty = false)
        {
            var s = string.Empty;
            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    var item = array.GetValue(i)?.ToString();
                    if (!excludeEmpty
                        || string.IsNullOrWhiteSpace(item))
                    {
                        if (s != string.Empty)
                            s += $"{delimiter}{item}";
                        else
                            s += item?.ToString();
                    }
                }
            }
            return s;
        }
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}
