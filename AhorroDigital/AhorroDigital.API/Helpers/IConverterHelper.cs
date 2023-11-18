using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Models;

namespace AhorroDigital.API.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model,Guid imageId, bool isNew);
        UserViewModel ToUserViewModel(User user);

        Task<Saving> ToSavingAsync(SavingViewModel model, bool isNew);
        SavingViewModel ToSavingViewModel(Saving saving);

        Task<Loan> ToLoanAsync(LoanViewModel model, Guid imageId, bool isNew);
        LoanViewModel ToLoanViewModel(Loan saving);

        Task<Payments> ToPaymentsPlanAsync(PaymentsPlantViewModel model, Guid imageId, bool isNew);

        Task<Payments> ToPaymentsAsync(NewPaymentViewModel model, Guid imageId,  bool isNew);

        //LoanViewModel ToPaymentsViewModel(Payments payments);

        List<Payments> ToConvertPaymentsEdit(int id);

        Task<Retreat> ToRetreatAsync(RetreatViewModel model, Guid imageId, bool isNew);
    }
}
