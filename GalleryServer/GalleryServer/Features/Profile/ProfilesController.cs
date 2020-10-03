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
        private const int ItemsPerPage = 3;

        private readonly IProfilesService profiles;
        private readonly ICurrentUserService currentUser;

        public ProfilesController(IProfilesService profiles, ICurrentUserService currentUser)
        {
            this.profiles = profiles;
            this.currentUser = currentUser;
        }
                
        [HttpGet("{userId}")]
        public async Task<ActionResult> Details(string userId)
        {
            var userInfo = this.profiles.UseGetInfoById(userId);            

            var result = new UserDetails
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email,                
            };

            return Accepted(result);
        }

        [Authorize]
        public async Task<ActionResult> Details()
        {
            var userId = this.currentUser.GetId();
            var userInfo = this.profiles.UseGetInfoById(userId);

            var result = new UserDetails
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email,
            };

            return Accepted(result);
        }

        [HttpGet("{all}")]
        public async Task<ActionResult> UserPosts(bool all = false)
        {
            var userId = this.currentUser.GetId();
            var userPosts = this.profiles.GetUserPosts(userId, all, ItemsPerPage);

            var result = new UserPosts
            {
                Posts = userPosts.Posts                
            };

            return Ok(result);
        }

        public async Task<ActionResult> LikedPosts(int page = 1)
        {
            var userId = this.currentUser.GetId();            
            var likedPosts = this.profiles.GetLikedPosts(userId);

            var result = new LikedPosts
            {                
                Posts = likedPosts
            };

            return Ok(result);
        }

        [HttpGet("{input}")]
        public async Task<ActionResult> Search(string input)
        {
            var users = this.profiles.GetUsers(input);

            return Ok(users);
        }
    }
}
