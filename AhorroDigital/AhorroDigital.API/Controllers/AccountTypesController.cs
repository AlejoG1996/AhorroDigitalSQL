using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;
using Microsoft.AspNetCore.Authorization;

namespace AhorroDigital.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountTypesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public AccountTypesController(DataContext context, IFlashMessage flasher)
        {
            _context = context;
            _flashMessage = flasher;
        }


        public async Task<IActionResult> Index()
        {
            List<AccountType>? AC;
            AC = await _context.AccountTypes
               .Include(u => u.Users)
                .ToListAsync();
            for (int i = 0; i < AC.Count(); i++)
            {
                int Cont = _context.Users.Where(o => o.AccountType.Name == AC[i].Name).Count();
               AC[i].NumberRegister = Cont;
            }
            return View(AC);
        }




        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(accountType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se registro exitosamente el  tipo de cuenta bancaria.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de cuenta bancaria.");

                    }
                    else
                    {
                        _flashMessage.Danger(string.Empty, dbUpdateException.InnerException.Message);


                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(string.Empty, exception.Message);


                }
            }

            return View(accountType);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccountTypes == null)
            {
                return NotFound();
            }

            AccountType accountType = await _context.AccountTypes.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }
            return View(accountType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccountType accountType)
        {
            if (id != accountType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se editó exitosamente el  tipo de cuenta bancaria.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de cuenta bancaria.");

                    }
                    else
                    {
                        _flashMessage.Danger(string.Empty, dbUpdateException.InnerException.Message);


                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(string.Empty, exception.Message);


                }

            }
            return View(accountType);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountType accountType = await _context.AccountTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountType == null)
            {
                return NotFound();
            }

            _context.AccountTypes.Remove(accountType);
            await _context.SaveChangesAsync();
            _flashMessage.Info(string.Empty, "Se elimino exitosamente el  tipo de cuenta bancaria.");
            return RedirectToAction(nameof(Index));

        }

        public JsonResult GetImagenes(string Email)
        {
            User user = _context.Users
                 .FirstOrDefault(c => c.Email == Email);
            List<string> info = new List<string>();

            if (user == null || Email == null)
            {
                info.Add("http://localhost:44321/images/users/noimages.png");

            }
            else
            {
                info.Add(user.ImageFullPath.ToString());

            }





            return Json(info);
        }

    }
}
