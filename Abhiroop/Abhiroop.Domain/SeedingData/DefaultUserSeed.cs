using Abhiroop.Domain.Entities;
using Abhiroop.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abhiroop.Domain.SeedingData
{
    public static class DefaultUserSeed
    {
        public static async Task SeedAdminUserAsync(UserManager<User> userManager, RoleManager<Role> roleManger)
        {
            var defaultUser = new User
            {
                UserName = "Abhiroop",
                Email = "Abhiroop@gmail.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Abhiroop123!");
                await userManager.AddToRolesAsync(defaultUser, new List<string> { Roles.Admin.ToString()});
            }

        }

        public static async Task SeedRoleAsync(RoleManager<Role> roleManger)
        {
            if (!roleManger.Roles.Any())
            {
                await roleManger.CreateAsync(new Role(Roles.Admin.ToString(), "this is Admin Role"));
            }
        }

    }
}
