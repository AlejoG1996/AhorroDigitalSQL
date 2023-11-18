using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace AhorroDigital.API.Data.Entities
{
    public class SavingType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Ahorro")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Name { get; set; }



        [JsonIgnore]
        public ICollection<Saving>? Savings { get; set; }


        [Display(Name = "Vr. Minimo Ahorro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int MinValue { get; set; }

        [Display(Name = "Dias Para Retiro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public int NumberDays { get; set; }

        [Display(Name = "Porcentaje Ganancia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public double PorcentageWin { get; set; }


        [Display(Name = "Número de registros")]
        public int NumberRegister { get; set; }

        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Marks { get; set; }
    }
}
