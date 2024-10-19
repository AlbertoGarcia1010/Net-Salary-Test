using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Salary.Models.ViewModel
{
    public class LoginUserVM
    {
        [Required(ErrorMessage = "La Clave de Usuario es Requerida.")]
        [DisplayName("Usuario")]
        [StringLength(20)]
        public string usuario { get; set; }

        [Required(ErrorMessage = "La Contraseña es Requerida.")]
        [DisplayName("Contraseña")]
        [StringLength(20)]
        public string password { get; set; }
    }
}
