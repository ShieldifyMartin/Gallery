namespace GalleryServer.Features.Category
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Cats;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext data;
        private readonly IPostsService posts;
        private readonly IDeletableEntityRepository<Post> postsRepo;

        public CategoriesService(ApplicationDbContext data, IPostsService posts, IDeletableEntityRepository<Post> postsRepo)
        {
            this.data = data;
            this.posts = posts;
            this.postsRepo = postsRepo;
        }

        public CategoryResponseViewModel GetAll()
        {
            var categories = this.data
                .Categories
                .OrderBy(c => c.Posts.Count)
                .ToList();

            var categoriesWithPosts = new List<Category>();

            foreach (var category in categories)            
            {
                var posts = this.posts.GetByCategoryId(category.Id);

                category.Posts = posts;
                categoriesWithPosts.Add(category);
            }

            var result = new CategoryResponseViewModel
            {
                Categories = categoriesWithPosts
            };
            
            return result;
        }

        public List<Post> GetPostsByCategory(int id)
        {
            var posts = this.postsRepo
                .All()                
                .Where(p => p.CategoryId == id)
                .OrderByDescending(p => p.Likes)
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
