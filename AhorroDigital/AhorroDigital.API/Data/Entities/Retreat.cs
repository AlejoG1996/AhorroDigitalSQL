using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhorroDigital.API.Data.Entities
{
    public class Retreat
    {
        public int Id { get; set; }

        [Display(Name = "Fecha Solicitud")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateS { get; set; }

       
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Saving Saving { get; set; }

        [Display(Name = "Fecha Apr/Den")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateM { get; set; }

        [Display(Name = "Valor Retiro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Value { get; set; }



        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
      
        public string? Marks { get; set; }

        [Display(Name = "Observación Administrador")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
    
        public string? MarksAdmin { get; set; }

        [Display(Name = "Estado")]
        public string? State { get; set; }

   

        [Display(Name = "Comprobante")]
        public Guid ImageId { get; set; }

        [Display(Name = "Comprobante")]
        public string ImageFullPath => ImageId == Guid.Empty
                ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
                : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Administrador")]
       
        public string? UserAdmin { get; set; }


    }
}
