using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhorroDigital.API.Data.Entities
{
    public class Payments
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Loan Loan { get; set; }

        [Display(Name = "Id")]
        public int IdSec { get; set; }

        [Display(Name = "Fecha de pago")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        public string? Marks { get; set; }

        [Display(Name = "Observación Administrador")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        public string? MarksAdmin { get; set; }

        [Display(Name = "Tipo de Pago")]
        public string? PaymentType { get; set; }

        [Display(Name = "Vr Capital ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueCapital { get; set; }

        [Display(Name = "Vr Interest  ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueInt { get; set; }

        [Display(Name = "Dias en mora")]
        public double DayArrears { get; set; }

        [Display(Name = "Valor en Mora")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueArrears { get; set; }

        [Display(Name = "Valor Pagado ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Value { get; set; }

        [Display(Name = "Valor   Pendiente")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueP { get; set; }

        [Display(Name = "Estado")]
        public string? State { get; set; }



        [Display(Name = "Administrador")]
        [JsonIgnore]
        public User UserAdmin { get; set; }



       

        [Display(Name = "Comprobante")]
        public Guid ImageId { get; set; }

        [Display(Name = "Comprobante")]
        public string ImageFullPath => ImageId == Guid.Empty
                ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
                : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "IdPaymentPlan")]
        public int IdPaymentPlan { get; set; }




    }
}
