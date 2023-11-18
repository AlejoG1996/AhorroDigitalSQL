using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhorroDigital.API.Data.Entities
{
    public class AccountType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de cuenta Bancaria")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }

        [Display(Name = "Número de registros")]
        public int NumberRegister { get; set; }
    }
}
