using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;

namespace AhorroDigital.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SavingTypesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public SavingTypesController(DataContext context, IFlashMessage flasher)
        {
            _context = context;
            _flashMessage = flasher;
        }

        public async Task<IActionResult> Index()
        {
            List<SavingType>? ST;
            ST = await _context.SavingTypes
                .Include(u => u.Savings)
                .ToListAsync();


            int Cont = 0;
            for (int i = 0; i < ST.Count(); i++)
            {

               Cont = _context.Savings.Where(o => o.SavingTypes.Name == ST[i].Name).Count();


                ST[i].NumberRegister = Cont;
            }
            return View(ST);

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SavingType savingType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (savingType.MinValue < 0)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor minimo de ahorro igual o superior a $0");
                        return View(savingType);
                    }

                    if (savingType.NumberDays < 0)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor de días para retiro igual o superior a 0");
                        return View(savingType);
                    }

                    if (savingType.PorcentageWin < 0 | savingType.PorcentageWin > 100)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor de ganancia del ahorro entre 0% y 100%");
                        return View(savingType);
                    }
                    double porc = Convert.ToDouble(savingType.PorcentageWin);
                    savingType.PorcentageWin = porc;
                    _context.Add(savingType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se registro exitosamente el  tipo de ahorro.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de ahorro.");

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

            return View(savingType);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SavingTypes == null)
            {
                return NotFound();
            }

            SavingType savingType = await _context.SavingTypes.FindAsync(id);
            if (savingType == null)
            {
                return NotFound();
            }
            return View(savingType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SavingType savingType)
        {
            if (id != savingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (savingType.MinValue < 0)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor minimo de ahorro igual o superior a $0");
                        return View(savingType);
                    }

                    if (savingType.NumberDays < 0)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor de días para retiro igual o superior a 0");
                        return View(savingType);
                    }

                    if (savingType.PorcentageWin < 0 | savingType.PorcentageWin > 100)
                    {
                        _flashMessage.Danger(string.Empty, "Debes ingresar un valor de ganancia del ahorro entre 0% y 100%");
                        return View(savingType);
                    }
                    double porc = Convert.ToDouble(savingType.PorcentageWin);
                    savingType.PorcentageWin = porc;
                    _context.Update(savingType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se editó exitosamente el  tipo de ahorro.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de ahorro.");

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
            return View(savingType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SavingType savingType = await _context.SavingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savingType == null)
            {
                return NotFound();
            }

            _context.SavingTypes.Remove(savingType);
            await _context.SaveChangesAsync();
            _flashMessage.Info(string.Empty, "Se elimino exitosamente el  tipo de ahorro.");
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
