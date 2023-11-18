﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhorroDigital.API.Data.Entities
{
    public class Contribute
    {
        public int Id { get; set; }

       
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Saving Saving { get; set; }

        

        [Display(Name = "Fecha")]
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

        [Display(Name = "Valor  Ahorro Pendiente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Value { get; set; }

        [Display(Name = "Valor  Ahorro ")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueAvail { get; set; }

        [Display(Name = "Valor  Ahorro Denegado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueSlop{ get; set; }

        [Display(Name = "Valor  Ahorro Retirado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueRetreat { get; set; }


  


        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
                ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
                : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Estado")]
        public string? State { get; set; }

        [Display(Name = "Admin")]
        [JsonIgnore]
        public User UserAdmin { get; set; }
    }
}
