namespace EasyElasticSearch
{
    public interface ISearchProvider
    {
        IEsQueryable<T> Queryable<T>() where T: class;
    }
}