namespace GalleryServer.Features.Identity.Models
{
    using System;

    public class UserRequestModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PictureUrl { get; set; }        
    }
}
