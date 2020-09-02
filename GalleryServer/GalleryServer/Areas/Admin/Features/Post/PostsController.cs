namespace GalleryServer.Areas.Admin.Features.Post
{
    using GalleryServer.Features.Cats;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PostsController : AdminsApiController
    {
        private readonly ICurrentUserService currentUser;
        private readonly IPostsService posts;

        public PostsController(ICurrentUserService currentUser, IPostsService posts)
        {
            this.currentUser = currentUser;
            this.posts = posts;
        }

        public async Task<ActionResult> All()
        {
            var posts = this.posts.GetAllAdmin();

            return Accepted(nameof(this.All), posts);
        }

        [HttpPatch("{postId}")]
        public async Task<ActionResult> Update(string postId, UpdatePatchRequestModel model)
        {       
            var userId = this.currentUser.GetId();
            //var pictureUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, model.Picture);
            var pictureUrl = "";

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
