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
        private readonly IAdminService adminService;

        public DashboardController(IPostsService posts, IIdentityService identity, IAdminService adminService)
        {
            this.posts = posts;
            this.identity = identity;
            this.adminService = adminService;
        }        
        
        public async Task<ActionResult> GetDashboardInfo()
        {
            if(!await this.adminService.IsUserAdminAuthorized())
            {
                return BadRequest();
            }

            var postsCount = this.posts.GetAllPostsCount();
            var usersCount = this.identity.GetAllUsersCount();
            var chartData = this.adminService.GetChartData().Activities;

            var result = new DashboardResponseModel
            {
                PostsCount = postsCount,
                UsersCount = usersCount,
                ChartData = chartData
            };

            return Accepted(nameof(this.GetDashboardInfo), result);
        }
    }
}
