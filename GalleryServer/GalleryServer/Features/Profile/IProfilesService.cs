﻿namespace GalleryServer.Features.Profile
{    
    using GalleryServer.Features.Profile.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProfilesService
    {
        UserPosts GetUserPosts(string userId);

        UserDetails UseGetInfoById(string userId);

        Task<List<SearchUserResults>> GetUsers(string input);
    }
}