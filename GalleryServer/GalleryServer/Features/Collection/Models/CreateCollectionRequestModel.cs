namespace GalleryServer.Features.Collection.Models
{
    using System.ComponentModel.DataAnnotations;

    using static GalleryServer.Data.Validation.Collection;

    public class CreateCollectionRequestModel
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Name { get; set; }
    }
}
