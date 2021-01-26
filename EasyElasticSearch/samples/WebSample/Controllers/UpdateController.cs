using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    /// <summary>
    /// 修改操作
    /// </summary>
    public class UpdateController : BaseController
    {
        private readonly IUpdateProvider _updateProvider;
        
        public UpdateController(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }
        
        /// <summary>
        /// 根据Id修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult UpdateByKey(string id)
        {
            var record = new User
            {
                UserId = "1458487865768454",
                UserName = "Update"
            };
            _updateProvider.Update(id, record);
            return Ok("Success");
        }
    }
}