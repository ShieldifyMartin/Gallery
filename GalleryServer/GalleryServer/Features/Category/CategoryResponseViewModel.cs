namespace GalleryServer.Features.Category
{
    using GalleryServer.Data.Models;

    public class CategoryResponseViewModel
    {
        public Category category { get; set; }

        public Post posts { get; set; }
    }
}
