namespace GalleryServer.Features.Post.Models
{
    using GalleryServer.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DetailsGetRequestModel
    {
        [Required]
        public string Id { get; set; }
        
        public string? Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        public string ProfilePicture { get; set; }        

        [Required]
        public int Likes { get; set; }

        [Required]
        public bool? IsLiked { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }        
    }
}
