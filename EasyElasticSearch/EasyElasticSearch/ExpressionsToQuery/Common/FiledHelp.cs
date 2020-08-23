using System.Linq;

namespace EasyElasticSearch
{
    public class FiledHelp
    {
        public static string GetValues(object obj, string filed)
        {
            var type = obj.GetType().GetProperties().Where(x => x.Name == filed).Select(x => x.PropertyType.Name).FirstOrDefault();
            if (type != null)
            {
                filed = filed.ToFirstLower();
                return type switch
                {
                    "String" => filed + ".keyword",
                    _ => filed,
                };
            }

            return filed;
        }
    }
}
