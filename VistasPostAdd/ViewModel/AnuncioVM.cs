using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VistasPostAdd.ViewModel
{
    public class AnuncioVM
    {
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Required]
        [Display(Name = "Precio Venta")]
        public int PrecioVenta { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(10)]
        public string Estado { get; set; }
        [Required]
        public int Categoria { get; set; }

    }
}
