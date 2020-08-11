namespace GalleryServer.Features.Cats
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using System;
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

    }
}
