using Microsoft.AspNetCore.Identity;
using Shared.Constants;
using Shared.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var roles = new List<IdentityRole>()
            {
                new IdentityRole(ApplicationRoles.Superadmin),
                new IdentityRole(ApplicationRoles.Agent)
            };
            foreach (var role in roles)
            {
                if (roleManager.Roles.All(r => r.Name != role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var administrator = new ApplicationUser { UserName = "admin", Email = "lance@kmcmaggroup.com", EmailConfirmed = true };
            var devteam = new ApplicationUser { UserName = "devteam", Email = "info@kmcmaggroup.com", EmailConfirmed = true };
            var agent = new ApplicationUser { FirstName = "Fiaz", LastName = "Ahmad", UserName = "agent123", Email = "csharpguy786@gmail.com", EmailConfirmed = true };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Love2eat!");
                await userManager.AddToRolesAsync(administrator, new[] { ApplicationRoles.Superadmin });
            }
            if (userManager.Users.All(u => u.UserName != devteam.UserName))
            {
                await userManager.CreateAsync(devteam, "Love2eat!");
                await userManager.AddToRolesAsync(devteam, new[] { ApplicationRoles.Superadmin });
            }

            if (userManager.Users.All(u => u.UserName != agent.UserName))
            {
                await userManager.CreateAsync(agent, "Love2eat!");
                await userManager.AddToRolesAsync(agent, new[] { ApplicationRoles.Agent });
            }
        }

        public static async Task SeedInitialData(ApplicationDbContext context)
        {
            // Seed, if necessary
            //if (!context.Funds.Any())
            //{
            //    context.Funds.AddRange(
            //        new FundType { 
            //            Name = "Litigated", 
            //            Note = "", 
            //            IsActive = true,
            //            CreatedBy = "Seeded" ,
            //            Currency = "PHP",
            //            IsUpcoming = false,
            //            TargetRaise = 0.00
            //        },
            //        new FundType { 
            //            Name = "Non-litigated", 
            //            Note = "", 
            //            IsActive = true, 
            //            CreatedBy = "Seeded",
            //            Currency = "USD",
            //            IsUpcoming = false,
            //            TargetRaise = 0.00
            //        }
            //    );
            //}

            await context.SaveChangesAsync();
        }
    }
}
