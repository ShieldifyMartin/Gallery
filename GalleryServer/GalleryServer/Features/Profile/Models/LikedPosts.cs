namespace GalleryServer.Features.Profile.Models
{
    using GalleryServer.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LikedPosts
    {   
        [Required]
        public List<Post> Posts { get; set; }
    }
}
