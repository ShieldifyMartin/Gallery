﻿namespace GalleryServer.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
            : this(null)
        {
        }

        public User(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string PictureUrl { get; set; }        

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        #nullable enable
        public DateTime? ModifiedOn { get; set; }

        #nullable enable
        public string? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        #nullable enable
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }

        public IEnumerable<Post> Posts { get; } = new HashSet<Post>();
    }
}
