namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Infrastructure.Services;    
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext data;
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(ApplicationDbContext data, IDeletableEntityRepository<Post> postsRepository)
        {
            this.data = data;
            this.postsRepository = postsRepository;
        }                

        public List<Post> GetAll()
        {
            var posts = this.postsRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .ToList();
            
            return posts;
        }

        public List<Post> GetTop10()
        {
            var posts = this.postsRepository
                .All()
                .Take(10)
                .ToList();

            return posts;
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

            await this.postsRepository.AddAsync(post);
            await this.data.SaveChangesAsync();

            return post.Id;
        }

        public async Task<Result> UpdatePost(string userId, string postId, string? location, string description, string pictureUrl, int? categoryId)
        {
            var post = this.postsRepository
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

            this.postsRepository.Update(post);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<Result> DeletePost(string userId, string postId)
        {
            var post = this.postsRepository
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

            this.postsRepository.Delete(post);
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

            var post = this.postsRepository
                .All()
                .FirstOrDefault(p => p.Id == postId);

            post.Likes++;
            post.Votes.Add(vote);

            await this.data.SaveChangesAsync();
            
            return true;
        }        
    }
}
