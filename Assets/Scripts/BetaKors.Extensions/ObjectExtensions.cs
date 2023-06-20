using System.Reflection;

namespace BetaKors.Extensions
{
    public static class ObjectExtensions
    {
        public static object InvokeMethod(this object obj, string methodName, BindingFlags flags, params object[] parameters)
        {
            return obj.GetType().InvokeMethod(methodName, flags, parameters);
        }
    }
}
