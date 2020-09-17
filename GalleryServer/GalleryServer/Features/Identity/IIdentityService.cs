namespace GalleryServer.Features.Identity
{
    using GalleryServer.Infrastructure.Services;    
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        Task<Result> AddProfilePicture(string userId, string pictureUrl);
    }
}
