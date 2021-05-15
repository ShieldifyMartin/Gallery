namespace GalleryServer.Features.Category
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Post;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private int countPostsPerPage = 9;

        private readonly ApplicationDbContext data;
        private readonly IPostsService posts;
        private readonly IDeletableEntityRepository<Post> postsRepo;        

        public CategoriesService(ApplicationDbContext data, IPostsService posts, IDeletableEntityRepository<Post> postsRepo)
        {
            this.data = data;
            this.posts = posts;
            this.postsRepo = postsRepo;            
        }

        public List<Category> GetAll()
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
                  
            return categoriesWithPosts;
        }

        public List<Post> GetPostsByCategory(int id, int currentPage)
            => this.postsRepo
                .All()
                .Where(p => p.CategoryId == id)
                .OrderByDescending(p => p.Likes)           
                .Take((currentPage + 1) * this.countPostsPerPage)
                .ToList();

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
