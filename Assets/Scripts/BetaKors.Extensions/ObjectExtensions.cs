using System.Reflection;

namespace BetaKors.Extensions
{
    public static class ObjectExtensions
    {
        public static object InvokeMethod(this object obj, string methodName, BindingFlags flags, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, flags);
            return method.Invoke(obj, parameters);
        }
    }
}
