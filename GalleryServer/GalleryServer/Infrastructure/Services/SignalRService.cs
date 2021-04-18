namespace GalleryServer.Infrastructure.Services
{
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Post;
    using GalleryServer.Features.Post.Models;
    using Microsoft.AspNetCore.SignalR;
    using System.Linq;
    using System.Threading.Tasks;

    public class SignalRService : Hub, ISignalRService
    {
        private readonly IDeletableEntityRepository<Post> posts;        

        public SignalRService(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }      

        public async Task ReturnAllPosts()
        {
            var posts = this.posts
                .All()
                .Select(p => new GetAllGetRequestModel
                {
                    Id = p.Id,
                    Location = p.Location,
                    Description = p.Description,
                    Picture = p.Picture,
                    Likes = p.Likes,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy
                })
                .OrderByDescending(p => p.Likes)
                .Take(9)
                .ToList();
            await Clients.All.SendAsync("ReceivePosts", posts);
        }
    }
}
