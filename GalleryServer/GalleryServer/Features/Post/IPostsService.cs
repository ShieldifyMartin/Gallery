namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<string> Create(string description, string location, string pictureUrl, string userId);

        Task<List<Post>> GetAll();

        Task<List<Post>> GetTop10();
    }
}
