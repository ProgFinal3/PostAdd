using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PostAds.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class AppUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Imagen { get; set; }
        public bool Bloqueado { get; set; }

        //Relationship
        public List<Anuncio> Anuncios { get; set; }
    }
}
