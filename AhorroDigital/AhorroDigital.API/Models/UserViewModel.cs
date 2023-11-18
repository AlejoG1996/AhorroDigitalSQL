using AhorroDigital.API.Data.Entities;
using AhorroDigital.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Debes introducir un email válido.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PhoneNumber { get; set; }


        [Display(Name ="Foto")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Tipo de documento")]
        [Range(1,int.MaxValue, ErrorMessage ="Debes seleccionar  un tipo de documento.")]
        [Required(ErrorMessage ="El campo {0} es obligatorio.")]
        public int DocumentTypeId { get; set; }

        public IEnumerable<SelectListItem>? DocumentTypes { get; set; }


        [Display(Name = "Tipo de cuenta bancaria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar  un tipo de cuenta bancaria.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AccountTypeId { get; set; }

        public IEnumerable<SelectListItem>?AccountTypes { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

  


        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(150, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Address { get; set; }


        [DefaultValue("57")]
        [Display(Name = "Codigo País")]
        [MaxLength(5, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CountryCode { get; set; }

       

        [Display(Name = "Número cuenta bancaria")]
        [MaxLength(20, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string AccountNumber { get; set; }

        [Display(Name = "Banco")]
        [MaxLength(40, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Bank { get; set; }

      


        [Display(Name = "Usuario")]
        public UserType UserType { get; set; }


        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }



      

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
         : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";
    }
}
