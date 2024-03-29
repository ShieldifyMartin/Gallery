﻿namespace GalleryServer.Features.Post
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private int countPostsPerPage = 9;

        private readonly ApplicationDbContext data;
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly ICurrentUserService currentUser;
        private readonly IConfiguration configuration;        

        public PostsService(
            ApplicationDbContext data,
            IDeletableEntityRepository<Post> posts,
            ICurrentUserService currentUser,
            IConfiguration configuration)
        {
            this.data = data;
            this.posts = posts;
            this.currentUser = currentUser;
            this.configuration = configuration;            
        }

        public List<GetAllGetRequestModel> GetAll(int currentPage)
        {
            var posts = this.posts
                .All()
                .Select(p => new GetAllGetRequestModel
                {
                    Id = p.Id,
                    Location = p.Location,
                    Description = p.Description,
                    Picture = p.Picture,
                    Likes = p.Likes,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy
                })
                .OrderByDescending(p => p.Likes)
                .Take((currentPage + 1) * this.countPostsPerPage)
                .ToList();
            return posts;
        }

        public List<Post> GetAllAdmin(int currentPage)
            => this.posts
                .AllWithDeleted()
                .OrderByDescending(p => p.Likes)
                .Take((currentPage + 1) * this.countPostsPerPage)
                .ToList();

        public int GetAllPostsCount()
            => this.posts
                .AllWithDeleted()
                .ToList()
                .Count;

        public List<GetAllGetRequestModel> GetAllSortedByDate(int currentPage)
            => this.GetAll(currentPage)
                .OrderByDescending(p => p.CreatedOn)
                .ToList();

        public List<GetAllGetRequestModel> GetTop5()
            => this.posts
                .All()
                .OrderByDescending(p => p.Likes)
                .Take(5)
                .Select(p => new GetAllGetRequestModel
                {
                    Id = p.Id,
                    Location = p.Location,
                    Description = p.Description,
                    Picture = p.Picture,
                    Likes = p.Likes,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy
                })
                .ToList();

        public List<GetAllGetRequestModel> GetRandomPosts(int currentPage)
            => this.posts
                .All()
                .Select(p => new GetAllGetRequestModel
                {
                    Id = p.Id,
                    Location = p.Location,
                    Description = p.Description,
                    Picture = p.Picture,
                    Likes = p.Likes,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy
                })
                .Take(currentPage)
                .ToList();

        public ICollection<Post> GetByCategoryId(int categoryId)
            => this.posts
                .All()
                .OrderByDescending(p => p.Likes)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

        public DetailsGetRequestModel GetById(string id)
        {
            var userId = this.currentUser.GetId();

            var isLiked = this.data.Votes.Any(
                v => v.PostId == id && v.UserId == userId);

            var post = this.posts
                .All()
                .Select(p => new DetailsGetRequestModel
                {
                    Id = p.Id,
                    Location = p.Location,
                    Description = p.Description,
                    Picture = p.Picture,
                    Likes = p.Likes,
                    IsLiked = isLiked,
                    CategoryId = p.CategoryId,                    
                    UserName = p.User.UserName,
                    ProfilePicture = p.User.PictureUrl,
                    AuthorId = p.User.Id,
                    CreatedOn = p.CreatedOn
                })
                .FirstOrDefault(p => p.Id == id);

            return post;
        }

        public List<SearchPostRequestViewModel> Search(string input)
        {
            var inputToUpper = input.ToUpper();
            var posts = this.posts
                .All()
                .Where(p => p.Location.ToUpper().Contains(inputToUpper)
                            || p.Description.ToUpper().Contains(inputToUpper))
                .Select(p => new SearchPostRequestViewModel
                {
                    Id = p.Id,
                    Likes = p.Likes,
                    Location = p.Location,
                    Picture = p.Picture,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy
                })
                .ToList();

            return posts;
        }

        #nullable enable
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

        #nullable enable
        public async Task<Result> UpdatePost(string userId, string postId, string? location, string description, string? pictureUrl, int? categoryId)
        {
            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);
            
            if (post.UserId != userId)
            {
                return "You are not authorized to update this post.";
            }

            if (post == null)
            {
                return "This post cannot be found.";
            }

            post.Description = description;
            post.Location = location;

            if(pictureUrl != "")
            {
                post.Picture = pictureUrl;
            }

            post.CategoryId = categoryId;          

            this.posts.Update(post);
            await this.data.SaveChangesAsync();

            return true;
        }

        #nullable enable
        public async Task<Result> UpdatePostAdmin(string userId, string postId, string? location, string description, string pictureUrl, int? categoryId)
        {
            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);

            if (post == null)
            {
                return "This post cannot be found.";
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

        public async Task<Result> DeletePostAdmin(string userId, string postId)
        {
            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);

            if (post == null)
            {
                return "This post cannot be found.";
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
                return "This post is already liked from this user.";
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

            post.Likes += 1;
            post.Votes.Add(vote);

            await this.data.SaveChangesAsync();
            
            return true;
        }

        public async Task<Result> UnLikePost(string userId, string postId)
        {
            var vote = this.data
                .Votes
                .FirstOrDefault(
                    v => v.UserId == userId
                    && v.PostId == postId);

            if (vote is null)
            {
                return "This post is not liked from this user.";
            }

            this.data.Votes.Remove(vote);

            var post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == postId);

            post.Likes = post.Likes - 1;
            post.Votes.Remove(vote);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<Result> AddToCollection(string postId, int collectionId)
        {
            var post = this.posts
                .All()
                .First(p => p.Id == postId);

            var collection = this.data
                .Collections
                .First(c => c.Id == collectionId);

            collection.Posts.Add(post);
            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
