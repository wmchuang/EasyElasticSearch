using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ExpressionContext
    {
        private List<string> Fileds = new List<string>(0);

        public List<QueryBase> QueryList = new List<QueryBase>(0);

        public List<string> MemberNameList { get; set; } = new List<string>(0);

        public List<object> ValueList { get; set; } = new List<object>(0);

        public List<ExpressionType> OperatorList = new List<ExpressionType>(0);

        public QueryContainer GetQuery<T>() where T : class, new()
        {
            HandleField<T>();

            var result = new QueryContainer();

            for (int i = 0; i < QueryList.Count; i++)
            {
                var temp = QueryList.ElementAt(i);

                if (temp is TermQuery termQuery)
                {
                    termQuery.Field = Fileds.ElementAt(i);
                    termQuery.Value = ValueList.ElementAt(i);
                }
                else if (temp is TermRangeQuery termRangeQuery)
                {
                    termRangeQuery.Field = Fileds.ElementAt(i);
                    termRangeQuery.GreaterThan = !string.IsNullOrWhiteSpace(termRangeQuery.GreaterThan) ? ValueList.ElementAt(i).ToString() : string.Empty;
                    termRangeQuery.GreaterThanOrEqualTo = !string.IsNullOrWhiteSpace(termRangeQuery.GreaterThanOrEqualTo) ? ValueList.ElementAt(i).ToString() : string.Empty;
                    termRangeQuery.LessThan = !string.IsNullOrWhiteSpace(termRangeQuery.LessThan) ? ValueList.ElementAt(i).ToString() : string.Empty;
                    termRangeQuery.LessThanOrEqualTo = !string.IsNullOrWhiteSpace(termRangeQuery.LessThanOrEqualTo) ? ValueList.ElementAt(i).ToString() : string.Empty;
                }
                else if (temp is MatchPhraseQuery matchPhraseQuery)
                {
                    matchPhraseQuery.Field = Fileds.ElementAt(i);
                    matchPhraseQuery.Query = ValueList.ElementAt(i).ToString();
                }
                else if (temp is QueryStringQuery queryStringQuery)
                {
                    queryStringQuery.Fields = new[] { Fileds.ElementAt(i) };
                    queryStringQuery.Query = "*" + ValueList.ElementAt(i).ToString() + "*";
                }

                if (i == 0)
                {
                    result = temp;
                }
                else
                {
                    var @operator = OperatorList.ElementAt(i - 1);
                    if (@operator == ExpressionType.And || @operator == ExpressionType.AndAlso)
                    {
                        result = result && temp;
                    }
                    else if (@operator == ExpressionType.Or || @operator == ExpressionType.OrElse)
                    {
                        result = result || temp;
                    }
                    else
                    {
                        throw new System.Exception("拼接QueryContainer时发生错误");
                    }
                }
            }

            return result;
        }

        public void HandleField<T>() where T : class, new()
        {
            T t = new T();
            var temp = new Dictionary<string, string>();

            Fileds = MemberNameList.Select(x =>
            {
                var str = string.Empty;
                if (!temp.TryGetValue(x, out str))
                {
                    str = FiledHelp.GetValues(t, x);
                    temp.Add(x, str);
                }
                return str;
            }).ToList();
        }
    }
}
