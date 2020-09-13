using Microsoft.AspNetCore.Mvc;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}