namespace GalleryServer.Features.Post.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SearchModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}
