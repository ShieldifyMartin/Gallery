namespace GalleryServer.Areas.Admin.Features.Post
{
    using CloudinaryDotNet;
    using GalleryServer.Features.Cloudinary;
    using GalleryServer.Features.Post;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PostsController : AdminsApiController
    {        
        private readonly IPostsService posts;
        private readonly ICurrentUserService currentUser;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public PostsController(            
            IPostsService posts,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary,
            ICurrentUserService currentUser)
        {            
            this.posts = posts;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
            this.currentUser = currentUser;
        }

        public async Task<ActionResult> All()
        {
            var posts = this.posts.GetAllAdmin();

            return Accepted(nameof(this.All), posts);
        }        

        [HttpPost("{postId}")]
        public async Task<ActionResult> Update(string postId, [FromForm] UpdatePatchRequestModel model)
        {       
            var userId = this.currentUser.GetId();            
            var pictureUrl = "";

            if (!(model.Picture is null))
            {
                pictureUrl = await this.cloudinaryService.UploadAsync(cloudinary, model.Picture);
            }

            var result = await this.posts.UpdatePostAdmin(userId, postId, model.Location, model.Description, pictureUrl, model.CategoryId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }
            
            return Ok();
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult> Delete(string postId)
        {
            var userId = this.currentUser.GetId();
            var result = await this.posts.DeletePostAdmin(userId, postId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
