using System.Threading.Tasks;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    /// <summary>
    /// 删除操作
    /// </summary>
    public class DeleteController : BaseController
    {
        private readonly IDeleteProvider _deleteProvider;

        public DeleteController(IDeleteProvider deleteProvider)
        {
            _deleteProvider = deleteProvider;
        }

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
    }
}