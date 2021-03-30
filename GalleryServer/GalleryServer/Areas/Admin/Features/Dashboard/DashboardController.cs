namespace GalleryServer.Areas.Admin.Features.Dashboard
{
    using GalleryServer.Areas.Admin.Features.Dashboard.Models;
    using GalleryServer.Features.Identity;
    using GalleryServer.Features.Post;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DashboardController : AdminsApiController
    {
        private readonly IPostsService posts;
        private readonly IIdentityService identity;

        public DashboardController(IPostsService posts, IIdentityService identity)
        {
            this.posts = posts;
            this.identity = identity;
        }

        public ActionResult GetDashboardInfo()
        {
            var postsCount = this.posts.GetAllPostsCount();
            var usersCount = this.identity.GetAllUsersCount();

            var result = new DashboardResponseModel
            {
                PostsCount = postsCount,
                UsersCount = usersCount
            };

            return Accepted(nameof(this.GetDashboardInfo), result);
        }
    }
}
