using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name ="Contraseña Actual")]
        [Required(ErrorMessage ="El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="El campo {0} debe tener una longitud mínima de {1} carácteres. ")]
        public string OldPassword { get; set; }

        [Display(Name = "Nueva Contraseña ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener una longitud mínima de {1} carácteres. ")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmación de Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener una longitud mínima de {1} carácteres. ")]
        [Compare("NewPassword",ErrorMessage ="La nueva contraseña y la confirmación no son iguales.")]
        public string Confirm { get; set; }

    }
}
