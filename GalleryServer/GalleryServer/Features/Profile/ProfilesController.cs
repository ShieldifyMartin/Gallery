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
                Id = userInfo.Id,
                UserName = userInfo.UserName,
                Email = userInfo.Email,
                Picture = userInfo.Picture
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
                Id = userInfo.Id,
                UserName = userInfo.UserName,
                Email = userInfo.Email,
                Picture = userInfo.Picture
            };

            return Accepted(result);
        }

        [HttpGet("{all}")]
        public  ActionResult UserPosts(bool all = false)
        {
            var userId = this.currentUser.GetId();
            var userPosts = this.profiles.GetUserPosts(userId, all, ItemsPerPage);
            
            return Ok(userPosts);
        }

        [HttpGet("{all}/{id}")]
        public ActionResult UserPosts(string id, bool all = false)
        {
            var userPosts = this.profiles.GetUserPosts(id, all, ItemsPerPage);          

            return Ok(userPosts);
        }

        [HttpGet("{all}")]
        public ActionResult LikedPosts(bool all = false)
        {
            var userId = this.currentUser.GetId();
            var likedPosts = this.profiles.GetLikedPosts(userId, all, ItemsPerPage);           

            return Ok(likedPosts);
        }

        [HttpGet("{all}/{id}")]
        public ActionResult LikedPosts(string id, bool all = false)
        {
            var likedPosts = this.profiles.GetLikedPosts(id, all, ItemsPerPage);

            return Ok(likedPosts);
        }

        [HttpGet("{input}")]
        public async Task<ActionResult> Search(string input)
        {
            var users = this.profiles.GetUsers(input);

            return Accepted(users);
        }
    }
}
