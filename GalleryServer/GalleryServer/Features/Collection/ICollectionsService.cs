namespace GalleryServer.Features.Collection
{
    using GalleryServer.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GalleryServer.Data.Models;

    public interface ICollectionsService
    {
        Task<List<Collection>> All(string userId);

        Task<int> Add(string name, string userId);

        Task<Result> UpdatePost(int collectionId, string name);

        Task<Result> DeleteCollection(int collectionId);
    }
}
