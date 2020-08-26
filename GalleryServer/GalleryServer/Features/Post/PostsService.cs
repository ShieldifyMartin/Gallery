namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;    
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext data;
        private readonly IDeletableEntityRepository<Post> posts;

        public PostsService(ApplicationDbContext data, IDeletableEntityRepository<Post> posts)
        {
            this.data = data;
            this.posts = posts;
        }                

        public List<Post> GetAll()
        {
            var posts = this.posts
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .ToList();
            
            return posts;
        }

        public List<Post> GetTop5()
        {
            var posts = this.posts
                .All()
                .OrderByDescending(p => p.Likes)
                .ToList();

            return posts;
        }

        public DetailsGetRequestModel GetById(string id)
        {
            var post = this.posts
                .All()
                .Select(p => new DetailsGetRequestModel
                {
                    Id = p.Id,
                    Location = p.Location,
                    Description = p.Description,
                    Picture = p.Picture,
                    Likes = p.Likes,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,
                    CreatedOn = p.CreatedOn,
                    Votes = p.Votes                 
                })
                .FirstOrDefault(p => p.Id == id);

            return post;
        }

        public async Task<string> Create(string? location, string description, string pictureUrl, string userId, int? categoryId)
        {
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Location = location,
                Description = description,
                Picture = pictureUrl,
                UserId = userId,
                CategoryId = categoryId      
            };

            await this.posts.AddAsync(post);
            await this.data.SaveChangesAsync();

            return post.Id;
        }

        public async Task<Result> UpdatePost(string userId, string postId, string? location, string description, string pictureUrl, int? categoryId)
        {
            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);
            
            if (post == null)
            {
                return "This post cannot be found.";
            }

            if (post.UserId != userId)
            {
                return "You are not authorized to update this post.";
            }

            post.Description = description;
            post.Location = location;
            post.Picture = pictureUrl;
            post.CategoryId = categoryId;          

            this.posts.Update(post);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<Result> DeletePost(string userId, string postId)
        {
            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);

            if (post == null)
            {
                return "This post cannot be found.";
            }

            if (post.UserId != userId)
            {
                return "You are not authorized to delete this post.";
            }

            this.posts.Delete(post);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<Result> LikePost(string userId, string postId)
        {
            var userAlreadyLiked = await this.data
                .Votes
                .AnyAsync(
                    v => v.UserId == userId 
                    && v.PostId == postId);

            if (userAlreadyLiked)
            { 
                return "This post is already liked by this user.";
            }

            var vote = new Vote
            {
                PostId = postId,
                UserId = userId
            };

            await this.data.AddAsync(vote);

            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);

            post.Likes++;
            post.Votes.Add(vote);

            await this.data.SaveChangesAsync();
            
            return true;
        }        
    }
}
