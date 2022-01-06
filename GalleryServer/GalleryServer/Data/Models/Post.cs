namespace GalleryServer.Data.Models
{    
    using GalleryServer.Data.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;   
    using static Validation.Post;

    public class Post : DeletableEntity, IEntity
    {
        [Key]
        public string Id { get; set; }

        #nullable enable
        [MaxLength(MaxLocationLength)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string Picture { get; set; }

        public int Likes { get; set; } = 0;

        #nullable enable
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        #nullable enable
        public int? CollectionId { get; set; }

        public virtual Collection Collection { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        #nullable enable
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
