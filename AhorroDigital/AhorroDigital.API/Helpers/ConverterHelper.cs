using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Data;
using AhorroDigital.API.Models;

namespace AhorroDigital.API.Helpers
{
    public class ConverterHelper:IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<User> ToUserAsync(UserViewModel model,Guid imageId, bool isNew)
        {
            return new User
            {
                Address = model.Address,
                CountryCode = model.CountryCode,
                Document = model.Document,
                DocumentType = await _context.DocumentTypes.FindAsync(model.DocumentTypeId),
                Email = model.Email,
                FirstName = model.FirstName,
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                UserType = model.UserType,
                AccountType = await _context.AccountTypes.FindAsync(model.AccountTypeId),
                AccountNumber = model.AccountNumber,
                Bank = model.Bank,
               ImageId=imageId


            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Address = user.Address,
                CountryCode = user.CountryCode,
                Document = user.Document,
                DocumentTypeId = user.DocumentType.Id,
                DocumentTypes = _combosHelper.GetComboDocumentTypes(),
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                AccountTypeId = user.AccountType.Id,
                AccountTypes = _combosHelper.GetComboAccountTypes(),
                AccountNumber = user.AccountNumber,
                Bank = user.Bank,
               ImageId=user.ImageId,

              
            };
        }

        public async Task<Saving> ToSavingAsync(SavingViewModel model, bool isNew)
        {
            return new Saving
            {
                SavingTypes = await _context.SavingTypes.FindAsync(model.SavingTypeId),
                DateIni = model.DateIni,
                MinValue = model.MinValue,
                Id = isNew ? 0 : model.Id,
                Marks = model.Marks

            };
        }

        public SavingViewModel ToSavingViewModel(Saving saving)
        {
            return new SavingViewModel
            {
                SavingTypes = _combosHelper.GetComboSavingTypes(),
                SavingTypeId = saving.SavingTypes.Id,
                Id = saving.Id,
                DateIni = saving.DateIni,
                MinValue = saving.MinValue,
                Marks = saving.Marks,
                UserId = saving.User.Id
            };
        }

        public async Task<Loan> ToLoanAsync(LoanViewModel model, Guid imageId, bool isNew)
        {
            DateTime date = DateTime.UtcNow.AddDays(-1);
            if (model.State.Equals("Aprobado"))
            {
                date = DateTime.UtcNow.AddDays(-1);
            }
            else
            {
                date = model.DateS.AddDays(10);
            }

            return new Loan
            {
                LoanType = await _context.LoanTypes.FindAsync(model.LoanTypeId),
                User = await _context.Users.FindAsync(model.UserId),
                DateS = model.DateS,

                DateA = date,
                State = model.State,

                Id = isNew ? 0 : model.Id,
                Marks = model.Marks,
                MarksAdmin = model.MarksAdmin,
                Value = model.Value,
                ValueP = 0,
                ValueD = 0,

                Interest = model.Interest,
                Dues = model.Dues,
                ValueDues = Convert.ToInt16(model.Value / model.Dues),
                //ValueNextDues= (model.Value / model.Dues) + Convert.ToInt16(model.Value *( model.Interest/100)),
                ImageId = imageId,

            };
        }

        public LoanViewModel ToLoanViewModel(Loan loan)
        {
            return new LoanViewModel
            {
                LoanTypes = _combosHelper.GetComboLoanTypes(),
                LoanTypeId = loan.LoanType.Id,
                Id = loan.Id,
                DateS = loan.DateS,
                Value = loan.Value,
                ValueAvail = loan.User.AvailLoan,
                Interest = loan.Interest,
                Dues = loan.Dues,
                UserId = loan.User.Id,
                MarksAdmin = loan.MarksAdmin,
                Marks = loan.Marks,

                State = loan.State,
               ImageId=loan.ImageId,
            };
        }


        public async Task<Payments> ToPaymentsPlanAsync(PaymentsPlantViewModel model, Guid imageId, bool isNew)
        {


            Payments pt = new Payments
            {
                Loan = await _context.Loans.FindAsync(model.LoanId),
                Date = model.Date,
                Marks = model.Marks,
                MarksAdmin = model.MarksAdmin,
                PaymentType = model.PaymentType,
                ValueCapital = 0,
                ValueInt = 0,
                DayArrears = 0,
                ValueArrears = 0,
                Value = 0,
                ValueP = 0,
                State = model.State,
                IdPaymentPlan = model.IdPaymentPlan,
                Id = isNew ? 0 : model.Id,

                ImageId = imageId,

            };
            pt.IdSec = pt.Loan.Id;
            return pt;

        }

        public async Task<Payments> ToPaymentsAsync(NewPaymentViewModel model, Guid imageId,bool isNew)
        {


            Payments st = new Payments
            {
                Loan = await _context.Loans.FindAsync(model.LoanId),
                Date = model.Date,
                Marks = model.Marks,
                MarksAdmin = model.MarksAdmin,
                PaymentType = model.PaymentType,
                ValueCapital = 0,
                ValueInt = 0,
                DayArrears = 0,
                ValueArrears = 0,
                Value = 0,
                ValueP = 0,
                State = model.State,

                Id = isNew ? 0 : model.Id,

                ImageId=imageId

            };
            st.IdSec = st.Loan.Id;
            return st;
        }



        public List<Payments> ToConvertPaymentsEdit(int id)
        {
            List<Payments> list = null;
            list = _context.Payments.Where(x => x.Loan.Id == id).ToList();
            return list;
        }

        public async Task<Retreat> ToRetreatAsync(RetreatViewModel model, Guid imageId, bool isNew)
        {

            return new Retreat
            {

                Saving = await _context.Savings.FindAsync(model.SavingId),
                DateS = model.DateS,
                DateM = model.DateS,
                State = model.State,
                Id = isNew ? 0 : model.Id,
                Marks = model.Marks,
                MarksAdmin = model.MarksAdmin,
                Value = model.Value,
                ImageId = imageId,

            };
        }
    }
}
