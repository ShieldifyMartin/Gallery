namespace GalleryServer.Models.Identity
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class AddProfilePictureRequestModel
    {
        [Required]
        public IFormFile Picture { get; set; }
    }
}
