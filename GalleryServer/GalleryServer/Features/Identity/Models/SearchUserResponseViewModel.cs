namespace GalleryServer.Features.Post.Models
{
    using GalleryServer.Features.Identity.Models;
    using System.Collections.Generic;

    public class SearchUserResponseViewModel
    {
        public List<Identity.Models.SearchUserRequestViewModel> Users { get; set; }
    }
}
