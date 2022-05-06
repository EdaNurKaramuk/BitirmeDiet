using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;


using static DietCalculatorSystem.WebConstants.AdminConstants;

namespace DietCalculatorSystem.Infrastructure
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;

            var data = services.GetRequiredService<DietCalculatorDbContext>();

            data.Database.Migrate();

            SeedAdministrator(data, services);

            return app;
        }

        private static void SeedAdministrator(DietCalculatorDbContext data, IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@dcs.bg";
                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        UserName = "Admin",
                        FullName = "Admin",
                        Email = adminEmail
                    };

                    var balanced = new Diet();
                    var deficit = new Diet();
                    var surplus = new Diet();

                    var balancedDiet = new BalancedDiet
                    {
                        User = user,
                        UserId = user.Id,
                        Diet = balanced,
                        DietId = balanced.Id
                    };

                    var deficitDiet = new DeficitDiet
                    {
                        User = user,
                        UserId = user.Id,
                        Diet = deficit,
                        DietId = deficit.Id
                    };

                    var surplusDiet = new SurplusDiet
                    {
                        User = user,
                        UserId = user.Id,
                        Diet = surplus,
                        DietId = surplus.Id
                    };

                    user.BalancedDiet = balancedDiet;
                    user.BalancedDietId = balanced.Id;
                    user.DeficitDiet = deficitDiet;
                    user.DeficitDietId = deficit.Id;
                    user.SurplusDiet = surplusDiet;
                    user.SurplusDietId = surplus.Id;

                    data.Diets.AddRange(balanced, deficit, surplus);

                    await data.SaveChangesAsync();

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
