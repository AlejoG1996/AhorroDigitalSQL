using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;

namespace AhorroDigital.API.Controllers
{
   [Authorize(Roles = "Admin")]
    public class LoanTypesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public LoanTypesController(DataContext context, IFlashMessage flasher)
        {
            _context = context;
            _flashMessage = flasher;
        }

        public async Task<IActionResult> Index()
        {
            List<LoanType>? LT;
            LT = await _context.LoanTypes
               .Include(u => u.Users)
                .ToListAsync();
            for (int i = 0; i < LT.Count(); i++)
            {
              int Cont = _context.Loans.Where(o => o.LoanType.Name == LT[i].Name).Count();

               LT[i].NumberRegister = Cont;
            }
            return View(LT);

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (loanType.NumberDues <= 0)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor maximo de cuotas  igual o superior a 1");
                        return View(loanType);
                    }
                    if (loanType.Interes < 0 || loanType.Interes > 100)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor de interes entre  0% y 100%");
                        return View(loanType);
                    }
                    _context.Add(loanType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se registro exitosamente el  tipo de préstamo.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de préstamo.");

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

            return View(loanType);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoanTypes == null)
            {
                return NotFound();
            }

            LoanType loanType = await _context.LoanTypes.FindAsync(id);
            if (loanType == null)
            {
                return NotFound();
            }
            return View(loanType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanType loanType)
        {
            if (id != loanType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (loanType.NumberDues <= 0)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor maximo de cuotas  igual o superior a 1");
                        return View(loanType);
                    }
                    if (loanType.Interes <= 0 || loanType.Interes > 100)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor de interes entre  0% y 100%");
                        return View(loanType);
                    }
                    _context.Update(loanType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se editó exitosamente el  tipo de préstamo.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de préstamo.");

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
            return View(loanType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LoanType loanType = await _context.LoanTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanType == null)
            {
                return NotFound();
            }

            _context.LoanTypes.Remove(loanType);
            await _context.SaveChangesAsync();
            _flashMessage.Info(string.Empty, "Se elimino exitosamente el  tipo de préstamo.");
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
