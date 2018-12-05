using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAds.Models
{
    public class Anuncio
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int PrecioVenta { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicaccion { get; set; }
        public string Estado { get; set; }
        public bool Bloqueado { get; set; }

        //Relationship
        public string AppUserId { get; set; }
        public List<Imagen> Imagen { get; set; }
        public int CategoriaId { get; set; }
    }
}
