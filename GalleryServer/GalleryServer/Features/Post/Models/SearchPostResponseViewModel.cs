namespace GalleryServer.Features.Post.Models
{    
    using System.Collections.Generic;

    public class SearchPostResponseViewModel
    {
        public List<SearchPostRequestViewModel> Posts { get; set; }
    }
}
