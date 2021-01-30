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


        [HttpGet]
        public IActionResult Delete()
        {
            _deleteProvider.DeleteByQuery<UserWallet>(x => x.UserName == "U32");
            return Ok("Success");
        }
    }
}