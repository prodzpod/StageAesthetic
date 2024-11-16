using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace StageAesthetic
{
    public static class Util
    {
        public static T? TryFind<T>(this IEnumerable<T> arr, Predicate<T> match)
        {
            var l = arr.ToList();
            var i = l.FindIndex(match);
            if (i == -1) return default;
            return l[i];
        }
        public static int Occurance(this string str, char c) => Occurance(str, c.ToString());
        public static int Occurance(this string str, string substr) {
            var ret = 0;
            var b = str.IndexOf(substr);
            var i = 0;
            while (b != -1)
            {
                i = b + 1; ret++;
                b = str.IndexOf(substr, i);
            }
            return ret;
        }
        // copying from stackoverflow GO
        public static List<Type> FindAllDerivedTypes<T>()
        {
            return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }
        public static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            var baseType = typeof(T);
            return assembly
                .GetTypes()
                .Where(t =>
                    t != baseType &&
                    baseType.IsAssignableFrom(t)
                    ).ToList();

        }
        public static Transform GetDescendant(this Transform parent, params int[] idx)
        {
            var ret = parent;
            foreach (var i in idx) ret = ret.GetChild(i);
            return ret;
        }
    }
    public enum Stage
    {
        Stage1 = 1,
        Stage2 = 2,
        Stage3 = 3,
        Stage4 = 4,
        Stage5 = 5,
        Special = 6,
        Ending = 7,
    }
}
