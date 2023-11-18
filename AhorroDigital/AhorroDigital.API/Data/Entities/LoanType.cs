using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhorroDigital.API.Data.Entities
{
    public class LoanType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Prétamo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Cuotas Maximas")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int NumberDues { get; set; }

        [Display(Name = "Interes")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Interes { get; set; }

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }

        [Display(Name = "Número de registros")]
        public int NumberRegister { get; set; }


        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Marks { get; set; }
    }
}
