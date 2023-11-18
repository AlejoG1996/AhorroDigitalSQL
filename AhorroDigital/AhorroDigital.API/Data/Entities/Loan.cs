using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Data.Entities
{
    public class Loan
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Prestamo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public LoanType LoanType { get; set; }

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [JsonIgnore]
        public User User { get; set; }

        [Display(Name = "Fecha Solicitud")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateS{ get; set; }

        [Display(Name = "Fecha Apr/Den")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateA { get; set; }

        [Display(Name = "Estado")]
        public string? State { get; set; }


       

        [Display(Name = "Observación")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        public string ?Marks { get; set; }

        [Display(Name = "Observación Admin")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más  de {1} carácteres.")]
        [DataType(DataType.MultilineText)]
        public string ?MarksAdmin { get; set; }

        [Display(Name = "Valor Préstamo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Value { get; set; }

        [Display(Name = "Valor Préstamo Pendiente")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueP { get; set; }

        [Display(Name = "Valor Préstamo Denegado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueD { get; set; }


        [Display(Name = "Valor Cuota")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueDues { get; set; }


        [Display(Name = "Interes")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Interest { get; set; }

        [Display(Name = "# Cuotas")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Dues { get; set; }

       



        [Display(Name = "Comprobante")]
        public Guid ImageId { get; set; }

        [Display(Name = "Comprobante")]
        public string ImageFullPath => ImageId == Guid.Empty
                ? $"https://ahorrodigitalapi.azurewebsites.net/images/noimage.png"
                : $"https://ahorrodigitalimagenes.blob.core.windows.net/users/{ImageId}";


        public ICollection<PaymentPlan> Payments { get; set; }


        [Display(Name = "Valor Total Deuda")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueTotal => Payments == null ? 0 :
              (Payments.Sum(x => x.PendientePago) - Payments.Sum(x => x.TotalInterest)) + Convert.ToInt32(((Payments.Sum(x => x.PendientePago) - Payments.Sum(x => x.TotalInterest)) * Interest) / 100);

        [Display(Name = "Valor Total Mora")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueArrearst => Payments == null ? 0 :
            Payments.Sum(x => x.ValueArrears);

        [Display(Name = "Total Préstamos Pagado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueLoanPagado { get; set; }

        [Display(Name = "# Pagos")]
        public int PaymetsCount => Payments == null ? 0 : Payments.Count;

        [Display(Name = "# Pagos Pendientes")]
        public int PaymetsCountP => Dues - PaymetsCount;






        [Display(Name = "Total Disponible")]
       [DisplayFormat(DataFormatString = "{0:C2}")]
       public int TotalAvail => User == null ? 0 :
          User.AvailLoan;


        public ICollection<Payments> PaymentF { get; set; }


        [Display(Name = "Total Pagado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Total => PaymentF == null ? 0 :
           PaymentF.Sum(x => x.Value);
    }
}
