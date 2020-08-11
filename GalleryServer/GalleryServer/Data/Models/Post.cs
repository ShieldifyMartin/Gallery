namespace GalleryServer.Data.Models
{    
    using GalleryServer.Data.Models.Base;
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Validation.Post;

    public class Post : DeletableEntity, IEntity
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(MaxLocationLength)]
        public string Location { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string Picture { get; set; }        

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
