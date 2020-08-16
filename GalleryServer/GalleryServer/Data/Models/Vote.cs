namespace GalleryServer.Data.Models
{    
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
