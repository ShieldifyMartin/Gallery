﻿namespace GalleryServer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validation.Category;

    public class Category
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]        
        public string Title { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
