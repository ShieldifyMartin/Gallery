namespace GalleryServer.Features.Post
{
    using GalleryServer.Data.Models;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        List<GetAllGetRequestModel> GetAll();

        List<Post> GetAllAdmin();

        List<GetAllGetRequestModel> GetAllSortedByDate();

        List<GetAllGetRequestModel> GetTop5();

        ICollection<Post> GetByCategoryId(int categoryId);

        DetailsGetRequestModel GetById(string id);

        List<SearchPostRequestViewModel> Search(string input);

        Task<string> Create(string? location, string description, string pictureUrl, string userId, int? categoryId);
        
        Task<Result> UpdatePost(string userId, string postId, string? location, string description, string pictureUrl, int? categoryId);

        Task<Result> UpdatePostAdmin(string userId, string postId, string? location, string description, string pictureUrl, int? categoryId);

        Task<Result> DeletePost(string userId, string postId);

        Task<Result> DeletePostAdmin(string userId, string postId);

        Task<Result> LikePost(string userId, string postId);

        Task<Result> UnLikePost(string userId, string postId);
    }
}
