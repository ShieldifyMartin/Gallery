namespace GalleryServer.Features.Cloudinary
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;    
    using Microsoft.AspNetCore.Http;    
    using System.IO;    
    using System.Threading.Tasks;

    public class CloudinaryService : ICloudinaryService
    {
        public async Task<string> UploadAsync(Cloudinary cloudinary, IFormFile picture)
        {
            string link = "";

            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(picture.FileName, destinationStream)
                };

                var res = await cloudinary.UploadAsync(uploadParams);

                link = res.Url.AbsoluteUri;
            }            

            return link;
        }
    }
}
