namespace GalleryServer.Features.Cloudinary
{
    using CloudinaryDotNet;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;    

    public interface ICloudinaryService
    {
        Task<string> UploadAsync(Cloudinary cloudinary, IFormFile picture);
    }
}
