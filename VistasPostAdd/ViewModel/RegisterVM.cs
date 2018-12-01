using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VistasPostAdd.ViewModel
{
    public class RegisterVM
    {

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email{ get; set; }
        [Required]
        [DataType( DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display( Name ="Confirmar Contraseña")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
