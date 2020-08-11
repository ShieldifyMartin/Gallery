namespace GalleryServer.Features.Cats
{
    using CloudinaryDotNet;
    using GalleryServer.Controllers;
    using GalleryServer.Features.Cloudinary;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PostsController : ApiController
    {
        private readonly IPostsService posts;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICurrentUserService currentUser;
        private readonly Cloudinary cloudinary;

        public PostsController(
            IPostsService posts,
            ICloudinaryService cloudinaryService,
            ICurrentUserService currentUser,
            Cloudinary cloudinary)
        {
            this.posts = posts;
            this.cloudinaryService = cloudinaryService;
            this.currentUser = currentUser;
            this.cloudinary = cloudinary;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePostRequestModel model)
        {
            var userId = this.currentUser.GetId();
            //var pictureUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, model.Picture);
            var pictureUrl = "";
            var id = await this.posts.Create(model.Description, model.Location, pictureUrl, userId);

            return Created(nameof(this.Create), id);
        }
    }
}
