using System.Linq;
using System.Text.RegularExpressions;

namespace EasyElasticSearch
{
    public static class StringExtension
    {
        public static string GetIndex<T>(this string index) where T : class
        {
            return !string.IsNullOrWhiteSpace(index) ? index : typeof(T).Name.ToLower();
        }

        public static string ToFirstLower(this string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        /// <summary>
        ///     Convert a string to camel-case.
        /// </summary>
        /// <param name="value">Input string to be camel-cased.</param>
        /// <param name="culture">CultureInfo to be used to lower-case first character.</param>
        /// <returns>String that has been converted to camel-case.</returns>
        public static string ToCamelCase(this string value)
        {
            var words = Regex.Split(value, "(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])");
            return string.Concat(words.First().ToLowerInvariant(), string.Concat(words.Skip(1)));
        }
    }
}