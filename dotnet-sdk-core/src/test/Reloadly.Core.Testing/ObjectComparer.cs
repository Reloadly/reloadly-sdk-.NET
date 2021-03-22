using System;
using System.Collections.Generic;

namespace Reloadly.Core.Testing
{
    public static class ObjectComparer
    {
        public static bool AreListsEqual<TExpected, TActual>(IList<TExpected> expected, IList<TActual> actual)
            where TExpected : class
            where TActual : class
        {
            if (expected.Count != actual.Count) return false;

            for (int i = 0; i < expected.Count; i++)
            {
                if (!ArePropertiesEqual(expected[i], actual[i])) return false;
            }

            return true;
        }

        public static bool ArePropertiesEqual<T1, T2>(T1 self, T2 to)
            where T1 : class
            where T2 : class
        {
            if (self != null && to != null)
            {
                Type type1 = typeof(T1);
                Type type2 = typeof(T2);

                foreach (System.Reflection.PropertyInfo pi in type1.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    object? selfValue = type1?.GetProperty(pi.Name)?.GetValue(self, null);
                    object? toValue = type2?.GetProperty(pi.Name)?.GetValue(to, null);

                    if (selfValue != null && !selfValue.GetType().IsPrimitive &&
                        !(selfValue is string) && !(selfValue is DateTime) && !(selfValue is TimeSpan))
                    {
                        // todo consider comparing list contents and complex types
                        continue;
                    }

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }
                }

                return true;
            }

            return self == to;
        }
    }
}
