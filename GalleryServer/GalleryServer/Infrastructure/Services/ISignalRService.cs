namespace GalleryServer.Infrastructure.Services
{       
    using System.Threading.Tasks;

    public interface ISignalRService
    {
        Task SendMessage(string user, string message);

        Task ReturnAllPosts();
    }
}
