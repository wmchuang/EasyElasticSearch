 # 综述

 
 [github文档地址](https://wmchuang.github.io/EasyElasticSearch/#/)  
 [gitee文档地址](http://crole.gitee.io/easyelasticsearch/#/)

**Elasticsearch** 是一个分布式、高扩展、高实时的搜索与数据分析引擎。它能很方便的使大量数据具有搜索、分析和探索的能力,简称**ES**

----------

欢迎使用 **EasyElasticSearch**，它是一个操作**ElasticSearch**的基础类库。是对[NEST](https://github.com/elastic/elasticsearch-net)的进一步封装,所以在可以直接使用**NEST**的操作方法的同时，可以使用我封装的一些方法。主要提供了一些表达式函数查询的方法，使用起来特别简单方便。

其中表达式解析参考了[SqlSuger](https://github.com/sunkaixuan),特此感谢 !

* * *

# 1. 从零开始

## 1.1 Docker安装
1. **Step 1**: __安装必要的一些系统工具__
    
    `sudo yum install -y yum-utils device-mapper-persistent-data lvm2`
2. **Step 2**: __添加软件源信息__
    
    `sudo yum-config-manager --add-repo http://mirrors.aliyun.com/docker-ce/linux/centos/docker-ce.repo`
3. **Step 3**: __缓存并安装Docker-CE(可选)__
    
    `sudo yum makecache fast`
    `sudo yum -y install docker-ce`
4. **Step 4**: __开启Docker服务__
    
    `sudo service docker start`
5. **Step 5**: __设置Docker开机自启动__
    
    `systemctl enable docker`
6. **Step 6**: __启动Docker__
    
    `systemctl start docker`
7. **Step 7**: __查看版本__
    
    `docker --version`

## 1.2 Docker-Compose 安装
1. **Step 1**: __安装__
    
    `pip3 install docker-compose`
2. **Step 2**: __查看版本__
    
    `docker-compose version`

## 1.3 ES 安装
> elasticsearch.yml
```yaml
## Default Elasticsearch configuration from Elasticsearch base image.
## https://github.com/elastic/elasticsearch/blob/master/distribution/docker/src/docker/config/elasticsearch.yml
#
cluster.name: "docker-cluster"
network.host: 0.0.0.0

## X-Pack settings
## see https://www.elastic.co/guide/en/elasticsearch/reference/current/setup-xpack.html
#
xpack.license.self_generated.type: trial
xpack.security.enabled: false
xpack.monitoring.collection.enabled: false

```
> docker-compose.yml
```yaml
version: '3.2'

services:
  elasticsearch:
    restart: always
    image: elasticsearch:7.10.0
    volumes:
      - type: bind
        source: ./elasticsearch/config/elasticsearch.yml
        target: /usr/share/elasticsearch/config/elasticsearch.yml
        read_only: true
      - type: volume
        source: elasticsearch
        target: /usr/share/elasticsearch/data
    ports:
      - "9200:9200"
      - "9300:9300"
    environment:
      ES_JAVA_OPTS: "-Xmx256m -Xms256m"
      ELASTIC_PASSWORD: changeme
      # Use single node discovery in order to disable production mode and avoid bootstrap checks
      # see https://www.elastic.co/guide/en/elasticsearch/reference/current/bootstrap-checks.html
      discovery.type: single-node
networks:
  elk:
    driver: bridge

volumes:
  elasticsearch:
```

**安装**

    `docker-compose up --build -d --force-recreate`


## 1.4 elasticsearch-head安装
安装谷歌插件， 解压缩安装，[下载地址](https://github.com/wmchuang/google-/blob/master/extension_0_1_3.crx)

* * *
# 2.简单使用

## 查询
```csharp
[HttpGet]
public async Task<IActionResult> SearchAsync()
{
    var data = await _searchProvider.Queryable<UserWallet>().Where(x => x.UserName.Contains("5")).ToListAsync();
    return Success(data);
}
```


## 增加
```csharp
/// <summary>
/// 新增数据
/// </summary>
/// <returns></returns>
[HttpGet]
public async Task<IActionResult> InsertAsync()
{
    var user = new UserWallet
    {
        UserId = "A112312312311",
        UserName = $"U{DateTime.Now.Second.ToString()}",
        CreateTime = DateTime.Now,
        Money = 110m
    };
    await _indexProvider.InsertAsync(user);
    return Success();
}
/// <summary>
/// 批量新增
/// </summary>
/// <returns></returns>
[HttpGet]
public async Task<IActionResult> InsertRangeAsync()
{
    var users = new List<UserWallet>
    {
        new UserWallet
        {
            UserId = "B123123123",
            UserName = $"U{DateTime.Now.Second.ToString()}",
            CreateTime = DateTime.Now,
            Money = 80m
        },
        new UserWallet
        {
            UserId = "B4564123156",
            UserName = $"U{DateTime.Now.Second.ToString()}",
            CreateTime = DateTime.Now,
            Money = 90m
        }
    };
    await _indexProvider.InsertRangeAsync(users);
    return Success();
}
```
## 删除
```csharp
/// <summary>
/// 删除操作
/// </summary>
/// <returns></returns>
[HttpGet]
public async Task<IActionResult> Delete()
{
    await _deleteProvider.DeleteByQuery<UserWallet>(x => x.UserName == "U44" || x.UserName == "U26");
    return Success();
}
```
## .....

