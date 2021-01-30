namespace EasyElasticSearch
{
    public static class StringExtension
    {
        public static string GetIndex<T>(this string index) where T : class
        {
            return !string.IsNullOrWhiteSpace(index) ? index : typeof(T).Name.ToLower();
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFirstLower(this string str) 
        {
            return str.Substring(0, 1).ToLower() + str[1..];
        }
    }
}