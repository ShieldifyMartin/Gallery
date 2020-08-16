namespace GalleryServer.Features.Category
{
    using GalleryServer.Controllers;
    using Microsoft.AspNetCore.Mvc;    
    using System.Threading.Tasks;

    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        public async Task<ActionResult> All()
        {
            var categories = await this.categories.GetAll();

            return Accepted(nameof(this.All), categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult> Posts(int categoryId)
        {
            var posts = await this.categories.GetPostsByCategory(categoryId);

            return Accepted(nameof(this.Posts), posts);
        }
    }
}
