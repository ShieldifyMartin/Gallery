using GalleryServer.Infrastructure.Services;

namespace GalleryServer.Features.Cats
{
    public class CatsController
    {
        private readonly ICatsService cats;
        private readonly ICurrentUserService currentUser;

        public CatsController(
            ICatsService cats,
            ICurrentUserService currentUser)
        {
            this.cats = cats;
            this.currentUser = currentUser;
        }
    }
}
