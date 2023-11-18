using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AhorroDigital.API.Data;
using AhorroDigital.API.Data.Entities;
using System.Data;
using Vereyon.Web;
using Microsoft.AspNetCore.Authorization;

namespace AhorroDigital.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentTypesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public DocumentTypesController(DataContext context, IFlashMessage flasher)
        {
            _context = context;
            _flashMessage = flasher;
        }


        public async Task<IActionResult> Index()
        {
            List<DocumentType>? DC;
            DC = await _context.DocumentTypes
                .Include(u => u.Users)
                .ToListAsync();
            for (int i = 0; i < DC.Count(); i++)
            {
                int Cont = _context.Users.Where(o => o.DocumentType.Name == DC[i].Name).Count();
                DC[i].NumberRegister = Cont;
            }
            return View(DC);

        }




        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(documentType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se registro exitosamente el  tipo de documento.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de documento.");

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

            return View(documentType);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocumentTypes == null)
            {
                return NotFound();
            }

            DocumentType documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }
            return View(documentType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentType);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info(string.Empty, "Se editó exitosamente el  tipo de documento");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger(string.Empty, "Ya existe este tipo de documento.");

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
            return View(documentType);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentType documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentType == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentType);
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
