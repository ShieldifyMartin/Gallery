namespace GalleryServer.Features.Identity.Models
{
    using GalleryServer.Data.Models;
    using System.Collections.Generic;

    public class GetAllUsersResponseModel
    {
        public ICollection<UserRequestModel> Users { get; set; }
    }
}
