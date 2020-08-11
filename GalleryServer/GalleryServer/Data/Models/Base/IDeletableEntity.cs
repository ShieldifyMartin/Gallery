namespace GalleryServer.Data.Models.Base
{
    using System;

    public interface IDeletableEntity
    {
        DateTime? DeletedOn { get; set; }

        string DeletedBy { get; set; }

        bool IsDeleted { get; set; }
    }
}
