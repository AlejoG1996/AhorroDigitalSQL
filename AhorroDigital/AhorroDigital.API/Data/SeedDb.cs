using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Helpers;
using AhorroDigital.Common.Enums;
//using AhorroDigital.API.Helpers;
//using AhorroDigital.Common.Enums;

namespace AhorroDigital.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        public SeedDb(DataContext context , IUserHelper userHelper  )
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckAccountTypesAsync();
            await CheckDocumentTypesAsync();
            await CheckSavingTypesAsync();
            await CheckLoanTypesAsync();
            await CheckRolesAsycn();
            await CheckUserAsync("1010", "Alejo", "Galeano", "alejo@yopmail.com", "311 322 4620", "Calle Luna Calle Sol","0000000", "Bancolombia", UserType.Admin);
            await CheckUserAsync("1011", "Diego", "Galeano", "diego@yopmail.com", "311 322 4620", "Calle Luna Calle Sol", "111111111", "Bancolombia", UserType.User);

        }

        private async Task CheckUserAsync(string document, string firstName, string lastName, string email, string phoneNumber,
           string address, string accountnumber, string bank, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Address = address,
                    CountryCode = "57",
                    Document = document,
                    DocumentType = _context.DocumentTypes.FirstOrDefault(x => x.Name == "Cédula"),
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    AccountType = _context.AccountTypes.FirstOrDefault(x => x.Name == "Cuenta de Ahorro"),
                    UserName = email,
                    AccountNumber = accountnumber,
                    Bank = bank,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);


            }

        }
        private async Task CheckRolesAsycn()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckLoanTypesAsync()
        {
            if (!_context.LoanTypes.Any())
            {
                _context.LoanTypes.Add(new LoanType { Name = "Préstamo A1", NumberDues=2,Interes=3,Marks= "Este Préstamo tiene una taza de interes del 3% y se cancela a 2 cuotas mensuales" });
                _context.LoanTypes.Add(new LoanType { Name = "Préstamo A2", NumberDues = 7, Interes = 5, Marks = "Este Préstamo tiene una taza de interes del 5% y se cancela  hasta en  7 cuotas mensuales" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckSavingTypesAsync()
        {
            if (!_context.SavingTypes.Any())
            {
                _context.SavingTypes.Add(new SavingType { Name = "Ahorro programado", MinValue=20000, NumberDays=365, PorcentageWin= Convert.ToDouble(2), Marks="Este ahorro tiene como ahorro minimo $20.000, se  puede realizar retiro pasado los 365 días. y las ganancias generadas es del 2%" });
                _context.SavingTypes.Add(new SavingType { Name = "Ahorro digital", MinValue=0 , NumberDays = 365, PorcentageWin =Convert.ToDouble( 1), Marks = "Este ahorro tiene como ahorro minimo $0, se  puede realizar retiro pasado los 365 días. y las ganancias generadas es del 2%" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAccountTypesAsync()
        {
            if(!_context.AccountTypes.Any())
            {
                _context.AccountTypes.Add(new AccountType { Name = "Cuenta de Ahorro" });
                _context.AccountTypes.Add(new AccountType { Name = "Cuenta Corriente" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckDocumentTypesAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _context.DocumentTypes.Add(new DocumentType { Name = "Cédula" });
                
                await _context.SaveChangesAsync();
            }
        }
    }
}
