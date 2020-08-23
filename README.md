# 欢迎使用 EasyElasticSearch

**EasyElasticSearch 是一个操作ElasticSearch的基础类库**

Elasticsearch 是一个分布式、高扩展、高实时的搜索与数据分析引擎。它能很方便的使大量数据具有搜索、分析和探索的能力,简称ES

## EasyElasticSearch支持ES的增删改查的一个基础类库

### 增加:tw-1f357: :tw-1f357:
```csharp
	/// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            var log = new RegistryRecord
            {
                UserId = "1268436794379079680",
                UserName = "es",
                RegistryTime = DateTime.Now
            };

            _indexProvider.Index<RegistryRecord>(log);
            return Ok("Success");
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BulkAdd()
        {
            var logs = new List<RegistryRecord>
            {
                new RegistryRecord{
                      UserId = "1268436794379079680",
                      UserName = "Bulkes1",
                     RegistryTime = DateTime.Now
                },
                new RegistryRecord{
                      UserId = "1268436794379079680",
                      UserName = "Bulkes2",
                     RegistryTime = DateTime.Now
                },
            };

            _indexProvider.BulkIndex<RegistryRecord>(logs);
            return Ok("Success");
        }
```
