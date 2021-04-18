namespace GalleryServer.Areas.Admin.Features.Category
{
    using GalleryServer.Areas.Admin.Features.Category.Models;
    using GalleryServer.Areas.Admin.Features.Dashboard;
    using GalleryServer.Features.Category;    
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CategoriesController : AdminsApiController
    {
        private readonly ICategoriesService categories;
        private readonly IAdminService adminService;

        public CategoriesController(ICategoriesService categories, IAdminService adminService)
        {
            this.categories = categories;
            this.adminService = adminService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(CreatePostRequestModel model)
        {
            if (!await this.adminService.IsUserAdminAuthorized())
            {
                return BadRequest();
            }

            var category = await this.categories.Add(model.Title);

            return Accepted(nameof(this.Add), category);
        }        
    }
}
