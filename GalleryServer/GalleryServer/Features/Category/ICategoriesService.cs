namespace GalleryServer.Features.Category
{    
    using GalleryServer.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        CategoryResponseViewModel GetAll();

        List<Post> GetPostsByCategory(int id);

        Task<Category> Add(string title);
    }
}
