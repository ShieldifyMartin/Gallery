﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryServer.Data.Models
{
    public class Role : IdentityRole
    {
        public Role(string name)         
        {
            this.Name = name;
        }

        public override string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        #nullable enable
        public DateTime? ModifiedOn { get; set; }

        #nullable enable
        public string? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        #nullable enable
        public DateTime? DeletedOn { get; set; }

        public IEnumerable<Post> Posts { get; } = new HashSet<Post>();
    }
}
