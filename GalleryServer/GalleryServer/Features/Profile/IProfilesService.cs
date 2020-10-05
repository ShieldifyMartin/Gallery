namespace GalleryServer.Features.Profile
{
    using GalleryServer.Data.Models;
    using GalleryServer.Features.Profile.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProfilesService
    {
        UserPosts GetUserPosts(string userId, bool all, int itemsPerPage);

        UserDetails UseGetInfoById(string userId);

        Task<List<SearchUserResults>> GetUsers(string input);

        List<Post> GetLikedPosts(string userId, bool all, int itemsPerPage);
    }
}
