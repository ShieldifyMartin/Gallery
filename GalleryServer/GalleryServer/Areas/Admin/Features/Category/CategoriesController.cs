namespace GalleryServer.Areas.Admin.Features.Category
{
    using GalleryServer.Areas.Admin.Features.Category.Models;
    using GalleryServer.Features.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CategoriesController : AdminsApiController
    {

        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [HttpPost]
        public async Task<ActionResult> Add(CreatePostRequestModel model)
        {
            var category = await this.categories.Add(model.Title);

            return Accepted(nameof(this.Add), category);
        }
    }
}
