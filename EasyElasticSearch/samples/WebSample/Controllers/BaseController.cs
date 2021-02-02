using Microsoft.AspNetCore.Mvc;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        protected virtual OkObjectResult Success()
        {
            return Ok(new
            {
                IsSuccess = true
            });
        }
        
        protected virtual OkObjectResult Success<T>(T data, string message = "")
        {
            return Ok(new
            {
                IsSuccess = true,
                Message = message,
                Data = data
            });
        }
    }
}