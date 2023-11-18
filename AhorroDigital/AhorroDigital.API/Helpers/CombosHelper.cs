using AhorroDigital.API.Data;
using AhorroDigital.API.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AhorroDigital.API.Helpers
{
    public class CombosHelper:ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context) { _context = context; }

        public IEnumerable<SelectListItem> GetComboDocumentTypes()
        {
            List<SelectListItem> list = _context.DocumentTypes.Select(x=> new SelectListItem
            {
                Text = x.Name,
                Value=$"{x.Id}"
            }).OrderBy(x=>x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de documento...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboSavingTypes()
        {
            List<SelectListItem> list = _context.SavingTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de ahorro...]",
                Value = "0"
            });

            return list;
        }


        public IEnumerable<SelectListItem> GetComboAccountTypes()
        {
            List<SelectListItem> list = _context.AccountTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de cuenta bancaria...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboLoanTypes()
        {
            List<SelectListItem> list = _context.LoanTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de préstamo...]",
                Value = "0"
            });

            return list;
        }


    }
}
