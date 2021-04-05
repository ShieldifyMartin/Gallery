namespace GalleryServer.Areas.Admin.Features.Dashboard
{
    using GalleryServer.Areas.Admin.Features.Dashboard.Models;
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminService : IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly ICurrentUserService currentUser;
        private readonly ApplicationDbContext data;
        private readonly IDeletableEntityRepository<Post> posts;

        public AdminService(UserManager<User> userManager, ICurrentUserService currentUser, ApplicationDbContext data, IDeletableEntityRepository<Post> posts)
        {
            this.userManager = userManager;
            this.currentUser = currentUser;
            this.data = data;
            this.posts = posts;
        }

        public async Task<bool> IsUserAdminAuthorized()
        {
            var user = await this.userManager.FindByIdAsync(this.currentUser.GetId());
            var roles = await this.userManager.GetRolesAsync(user);
            if (roles[0] == "Admin")
            {
                return true;
            }

            return false;
        }

        public ChartDataModel GetChartData()
        {
            Dictionary<int, int> data = new Dictionary<int, int>();
            //|| DateTimeOffset.Parse(p.DeletedOn).UtcDateTime.Month == month)
            for (int i = 0; i < 3; i++)
            {
                var month = DateTime.UtcNow.Month - i;
                var postsCreatedDuringLastIMonth = this.posts
                    .All()
                    .Where(p =>
                        p.CreatedOn.Month == month
                        || p.ModifiedOn.Value.Date.Month == month
                        || p.IsDeleted && p.DeletedOn.Value.Date.Month == month)
                    .ToList();                

                int activitiesCount = postsCreatedDuringLastIMonth.Count;
                data.Add(month, activitiesCount);
            }

            return new ChartDataModel
            {
                Activities = data
            };
        }
    }
}
