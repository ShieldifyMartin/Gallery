namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data.Models;
    using GalleryServer.Infrastructure.Services;    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        List<Post> GetAll();

        List<Post> GetTop10();

        Task<string> Create(string? location, string description, string pictureUrl, string userId, int? categoryId);
        
        Task<Result> UpdatePost(string userId, string postId, string? location, string description, string pictureUrl, int? categoryId);

        Task<Result> LikePost(string userId, string postId);
    }
}
