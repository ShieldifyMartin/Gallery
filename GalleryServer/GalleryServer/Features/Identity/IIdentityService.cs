namespace GalleryServer.Features.Identity
{
    using GalleryServer.Features.Identity.Models;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);

        Task<Result> AddProfilePicture(string userId, string pictureUrl);

        List<UserRequestModel> GetAllUsers();

        List<UserRequestModel> GetAllUsersAdmin();

        int GetAllUsersCount();

        List<SearchUserRequestViewModel> Search(string input);
    }
}
