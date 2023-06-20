using System;
using System.Reflection;

namespace BetaKors.Extensions
{
    public static class TypeExtensions
    {
        public static object InvokeMethod(this Type type, string methodName, BindingFlags flags, params object[] parameters)
        {
            var method = type.GetMethod(methodName, flags);
            return method.Invoke(null, parameters);
        }
    }
}
