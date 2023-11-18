
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Data.Entities
{
    public class PaymentPlan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Loan Loan { get; set; }

        [Display(Name = "Fecha de pago")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha pago realizado")]
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DatePR { get; set; }

        [Display(Name = "Estado")]
        public string? State { get; set; }

        [Display(Name = "Vr Capital ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueCapital { get; set; }

        [Display(Name = "Vr Interest  ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueInt { get; set; }


        [Display(Name = "Vr Interest  G ")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueIntG { get; set; }

        [Display(Name = "Dias en mora")]
        public double DayArrears => State.Equals("Pendiente") ?  Math.Round(((DateTime.Now - Date).TotalDays) < 0 ? 0 : (DateTime.Now - Date).TotalDays):0;

        [Display(Name = "Dias en mora")]
        public double DayArrearsM { get; set; }


        [Display(Name = "Valor en Mora")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueArrears => State.Equals("Pendiente") ? Convert.ToInt16(((ValueCapital + ValueInt) * 0.04) * DayArrears):0;

        [Display(Name = "Valor en Mora")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueArrearsM { get; set; }

        [Display(Name = " Total Cuota a Pagar")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalValue => State.Equals("Pendiente") ?  ValueCapital + ValueInt + ValueArrears : 0;

        [Display(Name = "Total Pagado")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int ValueTP { get; set; }

        [Display(Name = "Pendiente Pago")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int PendientePago  { get; set; }

    [Display(Name = "Total Capital")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalCapital { get; set; }

        [Display(Name = "Total Interes")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int TotalInterest{ get; set; }


        [Display(Name = "Tipo de Pago")]
        public string? PaymentType { get; set; }

        [Display(Name = "Existe Pago")]
        public string? Pago { get; set; }
    }

}
