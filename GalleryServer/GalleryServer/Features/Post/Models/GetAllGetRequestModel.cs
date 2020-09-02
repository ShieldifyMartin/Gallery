namespace GalleryServer.Features.Post.Models
{
    using System;

    public class GetAllGetRequestModel
    {
        public string Id { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public int Likes { get; set; }

        public int? CategoryId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }        
    }
}
