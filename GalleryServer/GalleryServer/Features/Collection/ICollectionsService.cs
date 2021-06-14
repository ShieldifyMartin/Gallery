namespace GalleryServer.Features.Collection
{
    using GalleryServer.Infrastructure.Services;
    using System.Threading.Tasks;

    public interface ICollectionsService
    {
        Task<int> Add(string name, string userId);

        Task<Result> UpdatePost(int collectionId, string name);
    }
}
