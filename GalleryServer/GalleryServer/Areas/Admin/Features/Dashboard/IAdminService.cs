namespace GalleryServer.Areas.Admin.Features.Dashboard
{
    using GalleryServer.Areas.Admin.Features.Dashboard.Models;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<bool> IsUserAdminAuthorized();

        ChartDataModel GetChartData();
    }
}
