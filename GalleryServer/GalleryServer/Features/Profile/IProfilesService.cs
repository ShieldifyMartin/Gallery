namespace GalleryServer.Features.Profile
{    
    using GalleryServer.Features.Profile.Models;
    using System.Threading.Tasks;

    public interface IProfilesService
    {
        Task<UserPosts> GetUserPosts(string userId);
    }
}
