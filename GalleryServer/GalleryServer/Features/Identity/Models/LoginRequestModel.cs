namespace GalleryServer.Models.Identity
{
    using Microsoft.AspNetCore.Http;    
    using System.ComponentModel.DataAnnotations;    

    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }       

        [Required]
        public string Password { get; set; }
    }
}
