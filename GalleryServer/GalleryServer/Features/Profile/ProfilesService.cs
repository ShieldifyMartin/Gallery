namespace GalleryServer.Features.Profile
{
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Profile.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilesService : IProfilesService
    {
        private readonly IDeletableEntityRepository<Post> posts;
       
        public ProfilesService(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public async Task<UserPosts> GetUserPosts(string userId)
        {
            var posts = this.posts
                .All()
                .Where(p => p.UserId == userId)
                .ToList();

            var userInfo = new UserPosts
            {                
                Posts = posts
            };

            return userInfo;
        }
    }
}
