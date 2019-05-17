using System;

namespace DataService
{
    public static class StringUtils
    {

        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }
    }
}
