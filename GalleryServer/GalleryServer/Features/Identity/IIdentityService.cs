namespace GalleryServer.Features.Identity
{
    using GalleryServer.Features.Identity.Models;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;    
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        Task<Result> AddProfilePicture(string userId, string pictureUrl);

        Task<GetAllUsersResponseModel> GetAllUsers();

        SearchUserResponseViewModel Search(string input);
    }
}
