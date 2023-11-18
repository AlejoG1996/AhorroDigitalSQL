using AhorroDigital.Common.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;


namespace AhorroDigital.API.Data.Entities
{
    public class User:IdentityUser
    {
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DocumentType DocumentType { get; set; }


        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(150, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        public string Address { get; set; }


        [DefaultValue("57")]
        [Display(Name = "Codigo País")]
        [MaxLength(5, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CountryCode { get; set; }

        [Display(Name = "Tipo de cuenta bancaria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public AccountType? AccountType { get; set; }

        [Display(Name = "Número cuenta bancaria")]
        [MaxLength(20, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string AccountNumber { get; set; }

        [Display(Name = "Banco")]
        [MaxLength(40, ErrorMessage = "El campo {0}  no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Bank { get; set; }







        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath =>  ImageId == Guid.Empty
                ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
                : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";
         


        [Display(Name = "Usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        
        public ICollection<Saving> Savings { get; set; }

        [Display(Name = "# Ahorros")]
        public int SavingAccount => Savings == null ? 0 : Savings.Count();

        [Display(Name = "Total Ahorrado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalA => Savings == null ? 0 :
           Savings.Sum(x => x.Total);




        public ICollection<Loan> Loans { get; set; }



        [Display(Name = "# Préstamos")]
        public int LoanAccount => Loans == null ? 0 : Loans.Count();




        [Display(Name = "Total Préstamos Aprobado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueLoan => Loans == null ? 0 :
           Loans.Sum(x => x.Value);

        [Display(Name = "Total Préstamos Pendiente")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueLoanP => Loans == null ? 0 :
          Loans.Sum(x => x.ValueP);


        [Display(Name = "Total Préstamos Pagado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueLoanPag => Loans == null ? 0 :
         Loans.Sum(x => x.ValueLoanPagado);

        [Display(Name = "Total  Disponible Préstamos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int AvailLoan => Loans == null ? 0 :
        (TotalA * 2) - ValueLoan + ValueLoanPag;


        //[Display(Name = "# Pagos")]
        //public int RetreatAccount => Retreats == null ? 0 : Retreats.Count();


    }
}
