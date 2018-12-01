using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAds.Models
{
    public class Imagen
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }

        //Relationship
        public int AnuncioId { get; set; }
    }
}
