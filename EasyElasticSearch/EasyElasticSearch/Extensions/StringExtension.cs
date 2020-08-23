namespace EasyElasticSearch
{
    public static class StringExtension
    {
        public static string GetIndex<T>(this string index) where T : class
        {
            if (!string.IsNullOrWhiteSpace(index))
            {
                return index;
            }
            else
            {
                return typeof(T).Name.ToLower();
            }
        }

        public static string ToFirstLower(this string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }
    }
}