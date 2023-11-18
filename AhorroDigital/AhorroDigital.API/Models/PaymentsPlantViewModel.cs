using AhorroDigital.API.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class PaymentsPlantViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Pago a realizar")]
        [Range(1, int.MaxValue, ErrorMessage = "debes seleccionar un tipo de ahorro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int LoanId { get; set; }

        public IEnumerable<SelectListItem>? Loans { get; set; }

        [Display(Name = "Fecha de pago")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }


        [Display(Name = "Observación Usuario")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        public string? Marks { get; set; }

        [Display(Name = "Observación Administrador")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? MarksAdmin { get; set; }

        [Display(Name = "Tipo de Pago")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? PaymentType { get; set; }

        [Display(Name = "Valor Pagado ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int? Value { get; set; }

      

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? State { get; set; }


        public string? UserId { get; set; }





        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }


        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }





        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
         : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "IdPaymentPlan")]
        public int IdPaymentPlan { get; set; }

    }
}
