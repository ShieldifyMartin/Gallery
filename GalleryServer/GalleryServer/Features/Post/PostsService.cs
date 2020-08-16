namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext data;
        private readonly BaseRepository<Post> postsRepository;

        public PostsService(ApplicationDbContext data, BaseRepository<Post> postsRepository)
        {
            this.data = data;
            this.postsRepository = postsRepository;
        }

        public async Task<string> Create(string description, string? location, string pictureUrl, string userId)
        {
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Description = description,
                Location = location,
                Picture = pictureUrl,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.data.SaveChangesAsync();
            return post.Id;
        }

        public async Task<List<Post>> GetAll()
        {
            var posts = this.postsRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .ToList();
            
            return posts;
        }

        public async Task<List<Post>> GetTop10()
        {
            var posts = this.postsRepository
                    .All()
                    .Take(10)
                    .ToList();

            return posts;
        }
    }
}
