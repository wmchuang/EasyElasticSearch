using System.Linq;
using EasyElasticSearch.Entity.Mapping;
using Nest;

namespace EasyElasticSearch
{
    public class ExpressionContext
    {
        public ExpressionContext(MappingIndex mappingIndex)
        {
            Mapping = mappingIndex;
        }

        private MappingIndex Mapping { get; set; }

        public QueryContainer QueryContainer { get; set; }

        public string LastFiled { get; set; }

        public object LastValue { get; set; }

        public QueryBase LastQueryBase { get; set; }

        public void SetQuery()
        {
            HandleField();

            switch (LastQueryBase)
            {
                case TermQuery termQuery:
                    termQuery.Field = LastFiled;
                    termQuery.Value = LastValue;
                    break;
                // case TermRangeQuery termRangeQuery:
                //     termRangeQuery.Field = Fileds.ElementAt(i);
                //     termRangeQuery.GreaterThan = !string.IsNullOrWhiteSpace(termRangeQuery.GreaterThan) ? ValueList.ElementAt(i).ToString() : string.Empty;
                //     termRangeQuery.GreaterThanOrEqualTo = !string.IsNullOrWhiteSpace(termRangeQuery.GreaterThanOrEqualTo) ? ValueList.ElementAt(i).ToString() : string.Empty;
                //     termRangeQuery.LessThan = !string.IsNullOrWhiteSpace(termRangeQuery.LessThan) ? ValueList.ElementAt(i).ToString() : string.Empty;
                //     termRangeQuery.LessThanOrEqualTo = !string.IsNullOrWhiteSpace(termRangeQuery.LessThanOrEqualTo) ? ValueList.ElementAt(i).ToString() : string.Empty;
                //     break;
                // case MatchPhraseQuery matchPhraseQuery:
                //     matchPhraseQuery.Field = Fileds.ElementAt(i);
                //     matchPhraseQuery.Query = ValueList.ElementAt(i).ToString();
                //     break;
                // case QueryStringQuery queryStringQuery:
                //     queryStringQuery.Fields = new[] {Fileds.ElementAt(i)};
                //     queryStringQuery.Query = "*" + ValueList.ElementAt(i) + "*";
                //     break;
            }

            QueryContainer = LastQueryBase;
        }

        private void HandleField()
        {
            LastFiled = Mapping.Columns.First(x => x.PropertyName == LastFiled).SearchName;
        }
    }
}