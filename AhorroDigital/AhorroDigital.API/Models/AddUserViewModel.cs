using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class AddUserViewModel: EditUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Debes introducir un email valido.")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [MinLength(6,ErrorMessage ="El cambo {0} debe tener una longitud mínima de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Password { get; set; }

        [Display(Name = "Confirmación de Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "El cambo {0} debe tener una longitud mínima de {1} carácteres.")]
        [Compare("Password",ErrorMessage ="La contraseña y confirmación de contraseña no son iguales.")]
        public string PasswordConfirm { get; set; }
    }
}
