namespace GalleryServer.Areas.Admin.Features.Dashboard
{
    using GalleryServer.Data.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AdminService : IAdminService
    {        
        private readonly UserManager<User> userManager;
        private readonly ICurrentUserService currentUser;

        public AdminService(UserManager<User> userManager, ICurrentUserService currentUser)
        {            
            this.userManager = userManager;
            this.currentUser = currentUser;
        }

        public async Task<bool> IsUserAdminAuthorized()
        {
            var user = await this.userManager.FindByIdAsync(this.currentUser.GetId());
            var roles = await this.userManager.GetRolesAsync(user);
            if (roles[0] == "Admin")
            {
                return true;
            }

            return false;
        }
    }
}
