namespace GalleryServer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Collection
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
