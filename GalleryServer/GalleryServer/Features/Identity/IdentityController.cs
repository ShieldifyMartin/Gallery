namespace GalleryServer.Controllers
{
    using Data.Models;
    using Models.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Options;
    using GalleryServer.Features.Identity;
    using CloudinaryDotNet;
    using GalleryServer.Infrastructure.Services;
    using GalleryServer.Features.Cloudinary;    

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identity;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;
        private readonly AppSettings appSettings;
        private readonly ICurrentUserService currentUser;

        public IdentityController(
            UserManager<User> userManager,
            IIdentityService identity,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService,
            ICurrentUserService currentUser,
            IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.identity = identity;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
            this.appSettings = appSettings.Value;
            this.currentUser = currentUser;
        }

        [HttpPost]
        [AllowAnonymous]        
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
        
            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();
            }
        
            var token = this.identity.GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);
        
            return new LoginResponseModel
            {
                Token = token
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ProfilePicture([FromForm] AddProfilePictureRequestModel model)
        {
            var pictureUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, model.Picture);
            var result = await this.identity.AddProfilePicture(this.currentUser.GetId(), pictureUrl);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok(pictureUrl);
        }

        public async Task<ActionResult> GetAllUsers()
        {
            var users = await this.identity.GetAllUsers();

            return Accepted(nameof(this.GetAllUsers), users);
        }

        [HttpGet("{input}")]
        public async Task<ActionResult> Search(string input)
        {
            var posts = this.identity.Search(input);

            return Accepted(nameof(this.Search), posts);
        }
    }
}
