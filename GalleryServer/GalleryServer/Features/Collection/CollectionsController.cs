namespace GalleryServer.Features.Collection
{
    using GalleryServer.Controllers;
    using GalleryServer.Data;
    using GalleryServer.Features.Collection.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CollectionsController : ApiController
    {        
        private readonly ICurrentUserService currentUser;
        private readonly ICollectionsService collectionService;

        public CollectionsController(ICurrentUserService currentUser, 
            ICollectionsService collectionService)
        {            
            this.currentUser = currentUser;
            this.collectionService = collectionService;
        }
                
        [Authorize]
        public async Task<ActionResult> All()
        {
            var userId = this.currentUser.GetId();
            var collections = await this.collectionService.All(userId);

            return Accepted(nameof(this.All), collections);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(CreateCollectionRequestModel model)
        {
            var userId = this.currentUser.GetId();
            var id = await this.collectionService.Add(model.Name, userId);

            return Created(nameof(this.Add), id);
        }
    }
}
