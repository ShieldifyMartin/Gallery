namespace GalleryServer.Areas.Admin
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    //[Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class AdminsApiController : ControllerBase
    {
    }
}
