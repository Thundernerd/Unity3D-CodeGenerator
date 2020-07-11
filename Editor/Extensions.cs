using System;
using System.Collections.Generic;

namespace TNRD.CodeGeneration
{
    public static class Extensions
    {
        public static string ToPrintableString(this Type type)
        {
            if (!type.IsGenericList())
                return type.FullName;

            var name = type.FullName;
            name = name.Substring(0, name.IndexOf('`'));
            var argumentType = type.GetGenericArguments()[0];
            return string.Format("{0}<{1}>", name, argumentType.ToPrintableString());
        }

        public static bool IsClassOrStruct(this Type type)
        {
            if (type == typeof(string))
                return false;

            if (type == typeof(decimal))
                return false;

            if (type.IsClass)
                return true;

            return type.IsValueType && !type.IsPrimitive;
        }

        public static bool IsGenericList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
        
        public static string ToPrintableString(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Public:
                    return "public";
                case Accessibility.Private:
                    return "private";
                case Accessibility.Protected:
                    return "protected";
                case Accessibility.Internal:
                    return "internal";
                case Accessibility.ProtectedInternal:
                    return "protected internal";
                default:
                    throw new ArgumentOutOfRangeException("accessibility", accessibility, null);
            }
        }

        public static bool InRange<T>(this List<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }
    }
}
