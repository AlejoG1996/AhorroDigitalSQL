using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhorroDigital.API.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboDocumentTypes();
        IEnumerable<SelectListItem> GetComboSavingTypes();

        IEnumerable<SelectListItem> GetComboAccountTypes();

        IEnumerable<SelectListItem> GetComboLoanTypes();

     
    }
}
