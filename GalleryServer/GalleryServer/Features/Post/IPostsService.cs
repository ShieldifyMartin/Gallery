namespace GalleryServer.Features.Cats
{    
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<string> Create(string description, string location, string pictureUrl, string userId);
    }
}
