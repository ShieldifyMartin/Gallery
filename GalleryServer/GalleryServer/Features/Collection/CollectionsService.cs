﻿namespace GalleryServer.Features.Collection
{
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CollectionsService : ICollectionsService
    {
        private readonly ApplicationDbContext data;
        private readonly ICurrentUserService currentUser;

        public CollectionsService(ApplicationDbContext data, 
            ICurrentUserService currentUser)
        {
            this.data = data;
            this.currentUser = currentUser;
        }

        public async Task<List<Collection>> All(string userId)
        {
            var collections = this.data.Collections
                .Where(c => c.UserId == userId)
                .ToList();            

            return collections;
        }

        public async Task<int> Add(string name, string userId)
        {
            var collection = new Collection
            {                                                               
                Name = name,
                UserId = userId                
            };

            await this.data.Collections.AddAsync(collection);
            await this.data.SaveChangesAsync();

            return collection.Id;
        }

        public async Task<Result> UpdatePost(int collectionId, string name)
        {
            var userId = this.currentUser.GetId();
            var collection = this.data
                .Collections                
                .FirstOrDefault(c => c.Id == collectionId);

            if (collection.UserId != userId)
            {
                return "You are not authorized to update this collection.";
            }

            if (collection == null)
            {
                return "This collection cannot be found.";
            }

            collection.Name = name;
            await this.data.SaveChangesAsync();
            
            return true;
        }

        public async Task<Result> DeleteCollection(int collectionId)
        {
            var userId = this.currentUser.GetId();
            var collection = this.data.Collections               
                .FirstOrDefault(c => c.Id == collectionId);

            if (collection == null)
            {
                return "This post cannot be found.";
            }

            if (collection.UserId != userId)
            {
                return "You are not authorized to delete this post.";
            }

            this.data.Collections.Remove(collection);
            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
