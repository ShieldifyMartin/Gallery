namespace GalleryServer.Areas.Admin.Features.Profile
{
    using GalleryServer.Features.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : AdminsApiController
    {
        private readonly IIdentityService identity;

        public IdentityController(IIdentityService identity)
        {
            this.identity = identity;            
        }

        public async Task<ActionResult> GetAllUsers()
        {            
            var users = this.identity.GetAllUsersAdmin();

            return Accepted(nameof(this.GetAllUsers), users);
        }
    }
}
