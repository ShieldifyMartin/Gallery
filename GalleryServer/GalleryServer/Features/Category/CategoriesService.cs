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
        private readonly IDeletableEntityRepository<Post> posts;

        public CategoriesService(ApplicationDbContext data, IDeletableEntityRepository<Post> posts)
        {
            this.data = data;
            this.posts = posts;
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
            var posts = this.posts
                .All()
                .Where(p => p.CategoryId == id)                
                .ToList();

            return posts;
        }

        public async Task<Category> Add(string title)
        {
            var category = new Category
            {
                Title = title
            };

            await this.data.AddAsync(category);
            await this.data.SaveChangesAsync();

            return category;
        }
    }
}
