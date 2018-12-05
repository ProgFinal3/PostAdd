using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VistasPostAdd.Models
{
    public class AppDbContex: IdentityDbContext<AppUser>
    {
        public AppDbContex(DbContextOptions<AppDbContex> options)
            :base(options)
        {

        }

        public DbSet<Anuncio> Anuncio { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider,
        IConfiguration configuration)
        {
            UserManager<AppUser> userManager =
            serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string nombre = configuration["AdminUser:Nombre"];
            string apellido = configuration["AdminUser:Apellido"];
            string email = configuration["AdminUser:Email"];
            string password = configuration["AdminUser:Password"];
            string role = configuration["AdminUser:Role"];
            if (await userManager.FindByNameAsync(email) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = new AppUser
                {
                    UserName = email,
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email
                };
                IdentityResult result = await userManager
                .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

    }
}
