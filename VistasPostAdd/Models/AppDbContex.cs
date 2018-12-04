using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    }
}
