namespace GalleryServer.Data.Models.Base
{
    using System;

    public class DeletableEntity : IDeletableEntity
    {
        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
