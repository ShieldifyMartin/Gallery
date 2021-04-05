using System.Collections.Generic;

namespace GalleryServer.Areas.Admin.Features.Dashboard.Models
{
    public class DashboardResponseModel
    {
        public int PostsCount { get; set; }

        public int UsersCount { get; set; }

        public Dictionary<int, int> ChartData { get; set; }
    }
}
