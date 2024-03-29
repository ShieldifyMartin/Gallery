﻿namespace GalleryServer.Features.Profile.Models
{
    using GalleryServer.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserDetails
    {      
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        #nullable enable
        public string? Picture { get; set; }
    }
}
