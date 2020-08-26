namespace GalleryServer.Features.Profile
{
    using GalleryServer.Controllers;
    using GalleryServer.Features.Profile.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProfilesController : ApiController
    {
        private readonly IProfilesService profiles;
        private readonly ICurrentUserService currentUser;

        public ProfilesController(IProfilesService profiles, ICurrentUserService currentUser)
        {
            this.profiles = profiles;
            this.currentUser = currentUser;
        }
        
        [Authorize]
        public async Task<ActionResult> Details()
        {
            var userId = this.currentUser.GetId();
            var userPosts = await this.profiles.GetUserPosts(userId);

            var userName = currentUser.GetUserName();
            var userDetails = new UserDetailsGetRequestModel
            {                
                UserName = userName,
                Posts = userPosts.Posts
            };

            return Accepted(nameof(this.Details), userDetails);
        }
    }
}
