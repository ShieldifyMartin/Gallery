namespace GalleryServer.Features.Post.Models
{
    using Microsoft.AspNetCore.Http;   
    using System.ComponentModel.DataAnnotations;

    using static GalleryServer.Data.Validation.Post;

    public class CreatePostRequestModel
    {
        [Required]
        public IFormFile Picture { get; set; }

        #nullable enable
        [MaxLength(MaxLocationLength)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        #nullable enable
        public int? CategoryId { get; set; }
    }    
}
