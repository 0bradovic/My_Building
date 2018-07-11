using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA
{
    public static class Seed
    {
        public static async Task Initialize(MyDbContext context, UserManager<Account> userManager, RoleManager<MyRoleManager> roleManager)
        {
            // Ensure that the database exists and all pending migrations are applied.
            context.Database.Migrate();

            // Create roles
            string[] roles = new string[] { "SuperAdmin", "Admin", "Tenant" };
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new MyRoleManager(role));
                }
            }

            IdentityResult result1 = await userManager.CreateAsync(new Account() { UserName = "BooproAdmin", Email = "boopro@email.com" }, "Pa$$w0rd");
            IdentityResult result2 = await userManager.CreateAsync(new Account() { UserName = "JustAdmin", Email = "JustAdmin@email.com" }, "L0z!nka");
            
            // Ensure admin privileges
            Account SuperAdmin = await userManager.FindByEmailAsync("boopro@email.com");
            Account Admin = await userManager.FindByEmailAsync("JustAdmin@email.com");

            await userManager.AddToRoleAsync(SuperAdmin, roles[0]);
            await userManager.AddToRoleAsync(Admin, roles[1]);

        }
    }
}
