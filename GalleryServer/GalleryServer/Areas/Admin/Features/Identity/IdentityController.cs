namespace GalleryServer.Areas.Admin.Features.Profile
{
    using GalleryServer.Areas.Admin.Features.Dashboard;
    using GalleryServer.Features.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : AdminsApiController
    {
        private readonly IIdentityService identity;
        private readonly IAdminService adminService;

        public IdentityController(IIdentityService identity, IAdminService adminService)
        {
            this.identity = identity;
            this.adminService = adminService;
        }

        public async Task<ActionResult> GetAllUsers()
        {
            if (!await this.adminService.IsUserAdminAuthorized())
            {
                return BadRequest();
            }

            var users = this.identity.GetAllUsersAdmin();

            return Accepted(nameof(this.GetAllUsers), users);
        }
    }
}
