using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class SavingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Ahorro")]
        [Range(1, int.MaxValue, ErrorMessage="debes seleccionar un tipo de ahorro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SavingTypeId { get; set; }

        public IEnumerable<SelectListItem>? SavingTypes { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateIni { get; set; }

        [Display(Name = "Valor Minimo Ahorro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int MinValue { get; set; }

        public string? UserId { get; set; }

        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Marks { get; set; }

      
    }
}
