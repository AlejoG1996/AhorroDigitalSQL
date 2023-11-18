using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class RetreatViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ahorro")]
        public int? SavingId { get; set; }

        [Display(Name = "Fecha Solicitud")]
     
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime DateS { get; set; }

       

        [Display(Name = "Valor")]
      [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Value { get; set; }

       

    


        [Display(Name = "Observación Usuario")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
      
        public string? Marks { get; set; }

        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
      
        public string? MarksAdmin { get; set; }

        [Display(Name = "Foto")]

        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }





        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
         : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";



        [Display(Name = "Estado")]
        public string? State { get; set; }

    
    

      
    }
}
