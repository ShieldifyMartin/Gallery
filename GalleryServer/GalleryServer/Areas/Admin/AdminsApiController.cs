namespace GalleryServer.Areas.Admin
{    
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class AdminsApiController : ControllerBase
    {
    }
}
