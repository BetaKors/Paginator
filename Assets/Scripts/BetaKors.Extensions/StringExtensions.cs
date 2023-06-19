namespace BetaKors.Extensions
{
    public static class StringExtensions
    {
        public static string Remove(this string s, string toRemove)
        {
            return s.Replace(toRemove, "");
        }
    }
}
