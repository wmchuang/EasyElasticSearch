# 欢迎使用 EasyElasticSearch

**EasyElasticSearch 是一个操作ElasticSearch的基础类库**

Elasticsearch 是一个分布式、高扩展、高实时的搜索与数据分析引擎。它能很方便的使大量数据具有搜索、分析和探索的能力,简称ES

#### EasyElasticSearch是支持ES的增删改查的一个基础类库
##### 支持表达式函数查询,别名操作
##### 其中表达式解析参考了SqlSuger,特此感谢 https://github.com/sunkaixuan/SqlSugar

## 查询
```csharp
 [HttpGet]
 public IActionResult Search()
 {
     var data = _searchProvider.Queryable<User>().Where(x => x.UserName == "52").ToList();
     return Ok(data);
 }
```


## 增加
```csharp
 [HttpGet]
 public ActionResult Add()
 {
     var record = new RegistryRecord
     {
         UserId = "1268436794379079680",
         UserName = "es",
         RegistryTime = DateTime.Now
     };

     _indexProvider.Index<RegistryRecord>(record);
     return Ok("Success");
 }

 /// <summary>
 /// 批量新增
 /// </summary>
 /// <returns></returns>
 [HttpGet]
 public ActionResult BulkAdd()
 {
     var records = new List<RegistryRecord>
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

     _indexProvider.BulkIndex<RegistryRecord>(records);
     return Ok("Success");
 }
```
## 删除
```csharp
[HttpGet]
public IActionResult Delete()
{
    _deleteProvider.DeleteByQuery<RegistryRecord>(x => x.UserName == "Bulkes1");
    return Ok("Success");
}
```
## .....

