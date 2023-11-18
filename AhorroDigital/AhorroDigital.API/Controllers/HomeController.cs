using AhorroDigital.API.Data;
using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Helpers;
using AhorroDigital.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vereyon.Web;

namespace AhorroDigital.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;
        private readonly IUserHelper _userHelper;

        public HomeController(ILogger<HomeController> logger, DataContext context, IFlashMessage flasher, IUserHelper userHelper)
        {
            _logger = logger;
            _context = context;
            _flashMessage = flasher;
            _userHelper = userHelper;
        }

       
        public async Task<IActionResult> Index()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            ViewBag.Name = user.FullName;
            ViewBag.ContContribute = _context.Contributes.Count();
            ViewBag.ContLoan = _context.Loans.Count();
            ViewBag.ContPayments = _context.Payments.Count();
            ViewBag.ContRetreat = _context.Retreats.Count();
            ViewBag.ContUser = _context.Users.Count();
            ViewBag.ContSaving = _context.Savings.Count();
            ViewBag.ContContributev = _context.Contributes.Where(o => o.State.Equals("Aprobado")).Sum(x => x.ValueAvail);
            ViewBag.ContLoanv = _context.Loans.Where(o => o.State.Equals("Apronado")).Sum(x => x.Value);
            ViewBag.ContPaymentsv = _context.Payments.Where(o => o.State.Equals("Aprobado")).Sum(x => x.Value);
            ViewBag.ContRetreatv = _context.Retreats.Where(o => o.State.Equals("Aprobado")).Sum(x => x.Value);

            return View();
        }


        public IActionResult IndexHome()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
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