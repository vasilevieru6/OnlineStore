using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerWithAspIdAndEF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Models;
using OnlineShop.Models.Domain;

namespace IdentityServerWithAspIdAndEF
{
    public class SeedData
    {
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Seeding database...");

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                {
                    var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();
                    EnsureSeedData(context);
                }

                {
                    var context = scope.ServiceProvider.GetService<ShopDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    await SeedRole(scope);

                    var odry = await userMgr.FindByNameAsync("odryvieru");
                    if (odry == null)
                    {
                        odry = new User
                        {
                            FirstName = "Odri",
                            LastName = "Vieru",
                            UserName = "odryvieru",
                            Email = "vieruodry@gmail.com",
                        };
                        var result = await userMgr.CreateAsync(odry, "Pass123$");


                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = await userMgr.AddClaimsAsync(odry, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Odri Vieru"),
                            new Claim(JwtClaimTypes.GivenName, "Odri"),
                            new Claim(JwtClaimTypes.FamilyName, "Vieru"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Orhei', 'postal_code': 69118, 'country': 'Moldova' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                        });
                        await userMgr.AddToRoleAsync(odry, "Admin");

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Console.WriteLine("Odri created");                                               
                    }
                    else
                    {
                        Console.WriteLine("Odri already exists");
                    }
                }
            }
            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }

        private static async Task SeedRole(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<long>>>();

            var roles = new string[] { "Admin", "Customer" };

            foreach (var item in roles)
            {
                if (!await roleManager.RoleExistsAsync(item))
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(item));
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Config.GetClients().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Config.GetIdentityResources().ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Config.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }
        }
    }
}
