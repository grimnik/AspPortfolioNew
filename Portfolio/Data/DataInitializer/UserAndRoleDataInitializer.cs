using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data.DataInitializer
{
    public class UserAndRoleDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
    private static void SeedUsers(UserManager<IdentityUser> userManager)
    {
        if (userManager.FindByEmailAsync("Grimnik@Admin.com").Result == null)
        {

            IdentityUser user = new IdentityUser();
                PasswordHasher<IdentityUser> passwordHash = new PasswordHasher();
              
            user.UserName = "Grimnik@Admin.com";
            user.Email = "Grimnik@Admin.com";
              
               

            IdentityResult result = userManager.CreateAsync(user, "Admin_1").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
           
 
 
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
