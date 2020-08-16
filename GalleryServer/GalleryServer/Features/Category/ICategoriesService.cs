namespace GalleryServer.Features.Category
{    
    using GalleryServer.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<List<Category>> GetAll();

        Task<List<Post>> GetPostsByCategory(int id);        
    }
}
