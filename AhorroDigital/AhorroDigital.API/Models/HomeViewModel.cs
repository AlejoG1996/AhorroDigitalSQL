using AhorroDigital.API.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AhorroDigital.API.Models
{
    public class HomeViewModel
    {
       

        [Display(Name = "# Consignaciones Pendientes")]
      
        public int ContributesCount { get; set; }

        [Display(Name = "# Préstamos Pendientes")]

        public int Loans { get; set; }

        [Display(Name = "# Pagos Pendientes")]

        public int Payments { get; set; }

        [Display(Name = "# Retiros Pendientes")]

        public int Retreat { get; set; }


        [Display(Name = "Consignaciones")]

        public List<Contribute>? Contributes { get; set; }


    }
}
