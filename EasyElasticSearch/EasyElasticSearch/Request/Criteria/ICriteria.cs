namespace EasyElasticSearch
{
    /// <summary>
    /// Interface that all criteria must implement to be part of
    /// the query tree.
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// Name of this criteria as specified in the Elasticsearch DSL.
        /// </summary>
        string Name { get; }
    }
}
