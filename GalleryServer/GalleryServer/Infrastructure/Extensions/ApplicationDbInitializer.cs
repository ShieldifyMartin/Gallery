using GalleryServer.Data.Models;
using Microsoft.AspNetCore.Identity;

public static class ApplicationDbInitializer
{
    public static void SeedUsers(UserManager<User> userManager)
    {
        if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
        {
            User user = new User
            {
                UserName = "Admin",
                Email = "admin@admin.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "Admin").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}