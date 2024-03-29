﻿namespace GalleryServer.Features.Category
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
            var categories = this.categories.GetAll();

            return Accepted(nameof(this.All), categories);
        }

        [HttpGet("{categoryId}/{currentPage}")]        
        public async Task<ActionResult> Posts(int categoryId, int currentPage)
        {
            var posts = this.categories.GetPostsByCategory(categoryId, currentPage);

            return Accepted(nameof(this.Posts), posts);
        }
    }
}
