using AhorroDigital.API.Data;
using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Helpers;
using AhorroDigital.API.Models;
using AhorroDigital.Common.Enums;
using AhorroDigital.Common.Response;
using Core.Flash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Vereyon.Web;
using static System.Net.WebRequestMethods;

namespace AhorroDigital.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly IMailHelper _mailHelper;
        private readonly IBlobHelper _blobHelper;
        public AccountController(IUserHelper userHelper, DataContext context, ICombosHelper combosHelper, IFlashMessage flasher, IMailHelper mailHelper, IBlobHelper blobHelper)
        {
            _userHelper = userHelper;
            _flashMessage = flasher;
            _combosHelper = combosHelper;
            _context = context;
            _mailHelper = mailHelper;
            _blobHelper = blobHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    _flashMessage.Danger(string.Empty, "El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario.");
                }
                else
                {
                    _flashMessage.Danger(string.Empty, "Email o contraseña incorrectos.");

                }

            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("IndexHome", "Home");
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                DocumentTypes = _combosHelper.GetComboDocumentTypes(),
                AccountTypes = _combosHelper.GetComboAccountTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //validar img
                string filename = "";
                if (model.ImageFile == null)
                {

                    filename = "noexiste.png";
                }
                else
                {
                    filename = model.ImageFile.FileName;
                }
                if (!Regex.IsMatch(filename.ToLower(), @"^.*\.(jpg|gif|png|jpeg)$"))
                {
                    _flashMessage.Danger("la imagen debe ser tipo .jpg .gift .png .jpeg");
                    model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
                    model.AccountTypes = _combosHelper.GetComboAccountTypes();
                    return View(model);

                }

                // cedula no repetida
                User usertwo = await _context.Users
           .FirstOrDefaultAsync(x => x.Document == model.Document);
                if (usertwo != null)
                {

                    _flashMessage.Danger("El  Número de Documento ingresado ya está siendo usado.");
                    model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
                    model.AccountTypes = _combosHelper.GetComboAccountTypes();
                    return View(model);


                }
                else
                {

                    User usertwos = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == model.UserName);

                    if (usertwos != null)
                    {
                        _flashMessage.Danger("El  Email ingresado ya está siendo usado.");
                        model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
                        model.AccountTypes = _combosHelper.GetComboAccountTypes();
                        return View(model);
                    }
                }

                //almacenar foto
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }
                


                User user = await _userHelper.AddUserAsync(model, imageId, UserType.User);
               
                if(user==null)
                {
                    _flashMessage.Danger("Ya existe un usuario con la información ingresada, valide la información y intentelo nuevamente.");
                    model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
                    model.AccountTypes = _combosHelper.GetComboAccountTypes();
                    return View(model);
                }


                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                
                Response response = _mailHelper.SendMail(model.UserName,
                  "Ahorro Digital - Confirmación de cuenta",

                  $"<div style='width: 100%; height: 800px; background-color: #F2F4F4;' >" +
                        $"<div style='width: 100%; height: 130px; background-color: #27AE60; justify-content: center !important; align-items: center !important; text-align: center !important; color:#fff  !important;' >" +
                                $"<br>"+
                                $"<h1 style=' text-align: center !important; color: #fff !important; font-family: 'Roboto', sans-serif; padding-top: 20px; letter-spacing: 1px; font-size: 45px; '>" +
                                    user.FullName.ToUpper()+
                                $"</h1>"+
                                  $"<p style=' text-align: center !important; color: #fff !important; font-family: 'Roboto', sans-serif; padding-top: 20px; letter-spacing: 1px; font-size: 45px; '>" +
                                    "Felicidades falta poco para hacer parte de esta gran familia y obtener grandes beneficios" +
                                $"</p>" +
                              
                                 $"<br>" +
                        $"</div>" +
                                $"<div style='width: 100%; justify-content: center; align-items: center; text-align: center;' >" +
                                      $"<br>" +
                                      $"<img src='https://ahorrodigitalapi.azurewebsites.net/images/confirmacioncuentaemail.gif' style='width: 350px; margin: auto;  '  >" +
                                      $"<br>" +
                                        $"<h1 style=' text-align: center !important; font-family: 'Roboto', sans-serif; padding-top: 20px; letter-spacing: 1px; font-size: 45px;  color: #2ECC71 !important;'>" +
                                      "WOOW!" +
                                    $"</h1>" +
                                      
                                               $"<p style=' text-align: center; color: #616A6B; font-family: 'Roboto', sans-serif; ' >" +
                                                    "Estas a un solo paso de pertenecer a la familia de AHORRO DIGITAL " +
                                               $"</p>" +
                                               $"<p style=' text-align: center; color: #616A6B; font-family: 'Roboto', sans-serif;  ' >" +
                                                    "confirma tu cuenta y accede a todos nuestros servicios y beneficios." +
                                               $"</p>" +
                                               $"<br>" +
                                               $"<a  href = \"{tokenLink}\" style=' height: 90px !important; width: 400px !important; padding: 15px !important; background-color: #27AE60 !important; color:#fff !important; border-radius: 15px !important; font-size: 20px !important;  cursor: pointer !important; text-decoration:none !important;' >" +
                                                    "Confirmar Cuenta!" +
                                               $"</a>" +
                                $"</div>" +
                        
                  $"</div>" );

                if (response.IsSuccess)
                {
                    _flashMessage.Info ("Las instrucciones para habilitar su cuenta han sido enviadas al correo.");
                    model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
                    model.AccountTypes = _combosHelper.GetComboAccountTypes();

                    return View(model);
                }

                _flashMessage.Danger(string.Empty, response.Message);
            }
            _flashMessage.Danger("Asegúrese de ingresar toda la información.");
            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
            model.AccountTypes = _combosHelper.GetComboAccountTypes();
            return View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new()
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ImageId= user.ImageId,
                CountryCode=user.CountryCode,
                Id = user.Id,
                Document = user.Document,
                DocumentTypeId = user.DocumentType.Id,
                DocumentTypes = _combosHelper.GetComboDocumentTypes(),
                AccountNumber= user.AccountNumber,
                AccountTypeId=user.AccountType.Id,
                AccountTypes= _combosHelper.GetComboAccountTypes(),
                Bank=user.Bank,

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                //validar img
                string filename = "";
                if (model.ImageFile == null)
                {

                    filename = "noexiste.png";
                }
                else
                {
                    filename = model.ImageFile.FileName;
                }
                if (!Regex.IsMatch(filename.ToLower(), @"^.*\.(jpg|gif|png|jpeg)$"))
                {
                    _flashMessage.Danger("la imagen debe ser tipo .jpg .gift .png .jpeg");
                    model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
                    model.AccountTypes = _combosHelper.GetComboAccountTypes();
                    return View(model);

                }

                //almacenar foto
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                user.FirstName = model.FirstName;
                user.LastName=model.LastName;
                user.Address = model.Address;
                user.CountryCode = model.CountryCode;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageId = imageId;
                user.AccountType = await _context.AccountTypes.FindAsync(model.AccountTypeId);
                user.AccountNumber = model.AccountNumber;
                user.Bank=model.Bank;
                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }
            model.DocumentTypes=_combosHelper.GetComboDocumentTypes();
            model.AccountTypes = _combosHelper.GetComboAccountTypes();
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                if(user != null)
                {
                    IdentityResult result =await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(ChangeUser));
                    }
                    else
                    {
                        _flashMessage.Danger(string.Empty, result.Errors.FirstOrDefault().Description);

                    }
                }
                else
                {
                    _flashMessage.Danger("Usuario no encontrado");

                }
            }
            return View(model);
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email);
                if (user == null)
                {
                    _flashMessage.Danger(string.Empty, "El correo ingresado no corresponde a ningún usuario.");
                    return View(model);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);


                Response response = _mailHelper.SendMail(user.UserName,
                  "Ahorro Digital - Recuperar Contraseña",

                  $"<div style='width: 100%; height: 800px; background-color: #F2F4F4;' >" +
                        $"<div style='width: 100%; height: 130px; background-color: #27AE60; justify-content: center !important; align-items: center !important; text-align: center !important; color:#fff  !important;' >" +
                                $"<br>" +
                                $"<h1 style=' text-align: center !important; color: #fff !important; font-family: 'Roboto', sans-serif; padding-top: 20px; letter-spacing: 1px; font-size: 45px; '>" +
                                    "AHORRO DIGITAL"+
                                $"</h1>" +
                                  $"<p style=' text-align: center !important; color: #fff !important; font-family: 'Roboto', sans-serif; padding-top: 20px; letter-spacing: 1px; font-size: 45px; '>" +
                                    "El camino hacia la riqueza depende fundamentalmente de dos palabras: trabajo y ahorro – Benjamin Franklin" +
                                $"</p>" +

                                 $"<br>" +
                        $"</div>" + 
                                $"<div style='width: 100%; justify-content: center; align-items: center; text-align: center;' >" +
                                      $"<br>" +
                                      $"<img src='cid:imagen' style='width: 350px; margin: auto;  '  >" +
                                      $"<br>" +
                                        $"<h1 style=' text-align: center !important; font-family: 'Roboto', sans-serif; padding-top: 20px; letter-spacing: 1px; font-size: 45px;  color: #2ECC71 !important;'>" +
                                      "Recupera tu contraseña!" +
                                    $"</h1>" +

                                               $"<p style=' text-align: center; color: #616A6B; font-family: 'Roboto', sans-serif; ' >" +
                                                    "Haz clic en  el botón y restablece tu contraseña " +
                                               $"</p>" +
                                               $"<p style=' text-align: center; color: #616A6B; font-family: 'Roboto', sans-serif;  ' >" +
                                                    "Para que sigas disfrutando de todos nuestros servicios y beneficios " +
                                               $"</p>" +
                $"<br>" +
                                               $"<a  href = \"{link}\" style=' height: 90px !important; width: 400px !important; padding: 15px !important; background-color: #27AE60 !important; color:#fff !important; border-radius: 15px !important; font-size: 20px !important;  cursor: pointer !important; text-decoration:none !important;' >" +
                                                    "Recuperar Contraseña!" +
                                               $"</a>" +
                                $"</div>" +

                  $"</div>");

                if (response.IsSuccess)
                {
                    _flashMessage.Info("Las instrucciones para habilitar su cuenta han sido enviadas al correo.");
                   
                   
                    return View(model);
                }
               

            }

            return View(model);
        }


        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            User user = await _userHelper.GetUserAsync(model.UserName);
            if (user != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    _flashMessage.Info ( "Contraseña cambiada.");
                    return View();
                }

                _flashMessage.Danger("Error cambiando la contraseña.");
                return View(model);
            }

            _flashMessage.Danger( "Usuario no encontrado.");
            return View(model);
        }

        public JsonResult GetImagenes(string Email)
        {
            User user = _context.Users
                 .FirstOrDefault(c => c.Email == Email);
            List<string> info = new List<string>();

            if (user == null)
            {
                info.Add("http://localhost:5047/images/users/noimages.png");

            }





            info.Add(user.ImageFullPath.ToString());
            return Json(info);
        }




    }
}
