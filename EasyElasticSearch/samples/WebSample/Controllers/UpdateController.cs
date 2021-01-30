using System.Threading.Tasks;
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
        private readonly ISearchProvider _searchProvider;

        public UpdateController(IUpdateProvider updateProvider,ISearchProvider searchProvider)
        {
            _updateProvider = updateProvider;
            _searchProvider = searchProvider;
        }
        
        /// <summary>
        /// 根据Id修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateByKey(string id)
        {
            var record = new UserWallet
            {
                UserId = "1458487865768454",
                UserName = "Update"
            };
            await _updateProvider.UpdateAsync(id, record);
            return Ok("Success");
        }
        
        /// <summary>
        /// 根据Id修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateAsync(string id)
        {
            var userWallet = await _searchProvider.Queryable<UserWallet>().Where(x => x.Id == id).FirstAsync();

            if (userWallet == null) return Ok("Success");
            userWallet.UserName = "Update";
            await _updateProvider.UpdateAsync(id, userWallet);
            return Ok("Success");
        }
    }
}