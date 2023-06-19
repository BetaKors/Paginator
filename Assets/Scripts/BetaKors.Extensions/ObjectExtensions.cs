using System.Reflection;

namespace BetaKors.Extensions
{
    public static class ObjectExtensions
    {
        public static object InvokeMethod(this object obj, string methodName, object[] parameters)
        {
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var meth = obj.GetType().GetMethod(methodName, flags);
            return meth.Invoke(obj, parameters);
        }

        public static object InvokeMethod(this object obj, string methodName, object parameter)
        {
            return InvokeMethod(obj, methodName, new[] { parameter });
        }

        public static object InvokeMethod(this object obj, string methodName)
        {
            return InvokeMethod(obj, methodName, null);
        }
    }
}
