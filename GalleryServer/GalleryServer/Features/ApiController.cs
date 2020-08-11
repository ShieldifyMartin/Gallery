namespace GalleryServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
