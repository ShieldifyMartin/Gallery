namespace GalleryServer.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}
