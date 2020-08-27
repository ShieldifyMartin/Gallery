using System.ComponentModel.DataAnnotations;

namespace GalleryServer.Features.Profile.Models
{
    public class SearchUserResults
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
