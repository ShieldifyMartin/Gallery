namespace GalleryServer.Features.Profile
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Profile.Models;    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilesService : IProfilesService
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly ApplicationDbContext data;

        public ProfilesService(IDeletableEntityRepository<Post> posts, ApplicationDbContext data)
        {
            this.posts = posts;
            this.data = data;
        }

        public UserPosts GetUserPosts(string userId, bool all, int itemsPerPage)
        {
            var posts = this.posts
                .All()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedOn)                
                .ToList();

            if(!all)
            {
                posts = posts.Take(itemsPerPage).ToList();
            }

            var userPosts = new UserPosts
            {
                Posts = posts
            };

            return userPosts;
        }

        public UserDetails UseGetInfoById(string userId)
        {
            var user = this.data
                    .Users
                    .FirstOrDefault(u => u.Id == userId);

            var userInfo = new UserDetails
            {
                UserName = user.UserName,
                Email = user.Email
            };

            return userInfo;
        }

        public async Task<List<SearchUserResults>> GetUsers(string input)
        {
            var users = this.data
                .Users
                .Where(u => u.UserName.ToUpper().Contains(input.ToUpper()))
                .Select(u => new SearchUserResults
                    {
                        Id = u.Id,
                        Email = u.Email,
                        UserName = u.UserName
                    })
                .ToList();

            return users;
        }

        public List<Post> GetLikedPosts(string userId, bool all, int itemsPerPage)
        {
            var likedPosts = this.data
                .Votes
                .Where(v => v.UserId == userId)
                .Select(v => v.Post)
                .ToList();

            if (!all)
            {
                likedPosts = likedPosts.Take(itemsPerPage).ToList();
            }

            return likedPosts;
        }
    }
}
