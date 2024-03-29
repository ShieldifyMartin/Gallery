﻿namespace GalleryServer.Features.Post
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
        private readonly ICurrentUserService currentUser;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public PostsController(
            ICategoriesService categories,
            IPostsService posts,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary,
            ICurrentUserService currentUser)
        {
            this.categories = categories;
            this.posts = posts;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
            this.currentUser = currentUser;
        }

        [HttpGet("{currentPage}")]
        public ActionResult All(int currentPage)
        {
            var posts = this.posts.GetAll(currentPage);

            return Accepted(nameof(this.All), posts);
        }

        public ActionResult GetAllSortedByDate(int currentPage)
        {
            var posts = this.posts.GetAllSortedByDate(currentPage);

            return Accepted(nameof(this.GetAllSortedByDate), posts);
        }

        public ActionResult Top5()
        {
            var posts = this.posts.GetTop5();

            return Accepted(nameof(this.Top5), posts);
        }

        [HttpGet("{currentPage}")]
        public ActionResult GetRandom(int currentPage)
        {
            var posts = this.posts.GetRandomPosts(currentPage);

            return Accepted(nameof(this.GetRandom), posts);
        }

        [HttpGet("{postId}")]
        public ActionResult ById(string postId)
        {
            var post = this.posts.GetById(postId);                      

            return Accepted(nameof(this.ById), post);
        }

        [HttpGet("{input}")]
        public ActionResult Search(string input)
        {
            var posts = this.posts.Search(input);

            return Accepted(nameof(this.Search), posts);
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

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<ActionResult> UnLike(string postId)
        {
            var userId = this.currentUser.GetId();
            var result = await this.posts.UnLikePost(userId, postId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }        

        [HttpPost]
        [Authorize]        
        public async Task<ActionResult> Create([FromForm] CreatePostRequestModel model)
        {
            var categoryId = model.CategoryId;
            var userId = this.currentUser.GetId();
            var pictureUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, model.Picture);

            var id = await this.posts.Create(model.Location, model.Description, pictureUrl, userId, categoryId);

            return Created(nameof(this.Create), id);
        }  

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<ActionResult> Update(string postId, [FromForm] UpdatePatchRequestModel model)
        {
            var userId = this.currentUser.GetId();
            var pictureUrl = "";

            if (!(model.Picture is null))
            {
                pictureUrl = await this.cloudinaryService.UploadAsync(cloudinary, model.Picture);
            }

            var result = await this.posts.UpdatePost(userId, postId, model.Location, model.Description, pictureUrl, model.CategoryId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<ActionResult> Delete(string postId)
        {
            var userId = this.currentUser.GetId();
            var result = await this.posts.DeletePost(userId, postId);
            
            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("{postId}/{collectionId}")]
        [Authorize]
        public async Task<ActionResult> AddToCollection(string postId, int collectionId)
        {
            var result = await this.posts.AddToCollection(postId, collectionId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
