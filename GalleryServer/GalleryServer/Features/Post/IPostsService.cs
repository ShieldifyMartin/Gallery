namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data.Models;
    using GalleryServer.Infrastructure.Services;    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<string> Create(string description, string? location, string pictureUrl, string userId, int? categoryId);

        Task<List<Post>> GetAll();

        Task<List<Post>> GetTop10();

        Task<Result> LikePost(string userId, string postId);
    }
}
