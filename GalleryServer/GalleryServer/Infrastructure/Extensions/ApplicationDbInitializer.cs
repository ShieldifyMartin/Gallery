using GalleryServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class ApplicationDbInitializer
{
    public static async Task SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {        
        if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
        {
            User user = new User
            {
                UserName = "Admin",
                Email = "admin@admin.com"
            };
        
            IdentityResult result = userManager.CreateAsync(user, "Admin11!").Result;
            IdentityResult roleResult = await roleManager.CreateAsync(new IdentityRole("Administrator"));

            if (result.Succeeded && roleResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }    
}