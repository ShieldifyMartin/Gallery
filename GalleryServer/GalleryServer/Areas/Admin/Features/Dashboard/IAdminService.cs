namespace GalleryServer.Areas.Admin.Features.Dashboard
{
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<bool> IsUserAdminAuthorized();
    }
}
