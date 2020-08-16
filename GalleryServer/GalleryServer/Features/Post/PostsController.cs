namespace GalleryServer.Features.Cats
{
    using CloudinaryDotNet;
    using GalleryServer.Controllers;
    using GalleryServer.Features.Category;
    using GalleryServer.Features.Cloudinary;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PostsController : ApiController
    {
        private readonly ICategoriesService categories;
        private readonly IPostsService posts;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICurrentUserService currentUser;
        private readonly Cloudinary cloudinary;

        public PostsController(
            ICategoriesService categories,
            IPostsService posts,
            ICloudinaryService cloudinaryService,
            ICurrentUserService currentUser,
            Cloudinary cloudinary)
        {
            this.categories = categories;
            this.posts = posts;
            this.cloudinaryService = cloudinaryService;
            this.currentUser = currentUser;
            this.cloudinary = cloudinary;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreatePostRequestModel model)
        {
            var categoryId = model.CategoryId;
            var userId = this.currentUser.GetId();
            //var pictureUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, model.Picture);
            var pictureUrl = "";
            var id = await this.posts.Create(model.Description, model.Location, pictureUrl, userId, categoryId);

            return Created(nameof(this.Create), id);
        }

        public async Task<ActionResult> All()
        {
            var posts = await this.posts.GetAll();

            return Accepted(nameof(this.All), posts);
        }

        public async Task<ActionResult> Top10()
        {
            var posts = await this.posts.GetTop10();

            return Accepted(nameof(this.Top10), posts);
        }

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<ActionResult> Like(string postId)
        {
            var userId = this.currentUser.GetId();
            var result = await this.posts.LikePost(userId, postId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
