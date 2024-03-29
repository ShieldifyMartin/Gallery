﻿namespace GalleryServer.Features.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using GalleryServer.Data;
    using GalleryServer.Features.Identity.Models;
    using GalleryServer.Features.Post.Models;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext data;

        public IdentityService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(27),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<Result> AddProfilePicture(string userId, string pictureUrl)
        {
            var user = this.data
                .Users
                .FirstOrDefault(u => u.Id == userId);

            if (user.Id != userId)
            {
                return "You are not authorized to add profile picture to this post";
            }

            if (user == null)
            {
                return "This user cannot be found.";
            }

            user.PictureUrl = pictureUrl;

            await this.data.SaveChangesAsync();

            return true;
        }

        public List<UserRequestModel> GetAllUsers()
            => this.data
                .Users
                .Where(u => !u.IsDeleted)
                .Select(u => new UserRequestModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PictureUrl = u.PictureUrl
                })
                .OrderBy(u => u.UserName)
                .ToList();

        public List<UserRequestModel> GetAllUsersAdmin()
            => this.data
                .Users
                .Select(u => new UserRequestModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PictureUrl = u.PictureUrl
                })
                .OrderBy(u => u.UserName)
                .ToList();

        public int GetAllUsersCount()
            => this.data
                .Users
                .ToList()
                .Count;

        public List<SearchUserRequestViewModel> Search(string input)
            => this.data
                .Users
                .Where(u => u.UserName.ToUpper().Contains(input.ToUpper()))
                .Select(u => new SearchUserRequestViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PictureUrl = u.PictureUrl
                })
                .OrderBy(u => u.UserName)
                .ToList();
    }
}