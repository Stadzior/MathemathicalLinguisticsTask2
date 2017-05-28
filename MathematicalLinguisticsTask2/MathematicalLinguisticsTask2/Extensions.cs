using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalLinguisticsTask2
{
    public static class Extensions
    {
        public static bool IsMatchingAlphabet(this string word, HashSet<char> alphabet)
        {
            foreach (var character in word)
            {
                if (!alphabet.Contains(character))
                    return false;
            }
            return true;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action.Invoke(item);
        }
    }
}
