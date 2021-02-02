using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    /// <summary>
    /// 别名操作
    /// </summary>
    public class AliasController : BaseController
    {
        private readonly IAliasProvider _aliasProvider;
        public AliasController(IAliasProvider aliasProvider)
        {
            _aliasProvider = aliasProvider;
        }

        /// <summary>
        /// 添加别名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddAlias()
        {
            _aliasProvider.AddAliasAsync<UserWallet>("alias");
            return Success();
        }

        /// <summary>
        /// 删除别名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RemoveAlias()
        {
            _aliasProvider.RemoveAlias<UserWallet>("alias");
            return Success();
        }

    }
}