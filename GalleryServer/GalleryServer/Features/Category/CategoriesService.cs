namespace GalleryServer.Features.Category
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext data;
        private readonly BaseRepository<Post> postsRepository;

        public CategoriesService(ApplicationDbContext data, BaseRepository<Post> postsRepository)
        {
            this.data = data;
            this.postsRepository = postsRepository;
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = this.data
                .Categories                
                .ToList();

            return categories;
        }

        public async Task<List<Post>> GetPostsByCategory(int id)
        {
            var posts = this.data
                .Posts
                .Where(p => p.CategoryId == id)                
                .ToList();

            return posts;
        }        
    }
}
