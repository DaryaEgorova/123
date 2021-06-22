using Microsoft.AspNetCore.Mvc;
using Maganizer_Project.BLL.Interfaces;
using System.Threading.Tasks;
using Maganizer_Project.WEB.Models;
using Maganizer_Project.BLL.DTO;
using NETCore.MailKit.Core;
using Maganizer_Project.Models;

namespace Maganizer_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IEmailService emailService;
        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            this.accountService = accountService;
            this.emailService = emailService;
        }

        //GET
        [Route("SignIn")]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        //GET
        [Route("SignUp")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        //POST
        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var signUpDTO = new SignUpUserDTO()
                {
                    Username = signUpModel.Username,
                    Email = signUpModel.Email,
                    Password = signUpModel.Password,
                };

                var result = await accountService.CreateUser(signUpDTO);

                if (!result.Result.Succeeded)
                {
                    foreach(var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("SignUp", signUpModel);
                }

                //email verification
                return await SendMessage(result.VerificationCode, result.UserId, result.Email, result.Username);             
            }

            return View("SignUp", signUpModel);
        }

        public async Task<IActionResult> SendMessage(string verificationCode, string userId, string email, string username)
        {
            var code = verificationCode;
            var verificationLink = Url.Action(nameof(VerifyEmail), "Account", new { UserId = userId, code }, Request.Scheme, Request.Host.ToString());
            await emailService.SendAsync(email, "Maganizer account verification", $"<p>Hi {username},</p>" +
                $"<p>You can visit this link below to finish verification:</p>" +
                $"<a href=\"{verificationLink}\">Verify Email</a>" +
                $"<p>If you did not request this, please REPLY IMMEDIATELY as your account may be in danger.</p>", true);

            return RedirectToAction("EmailVerification", new EmailVerificationViewModel()
            {
                VerificationCode = verificationCode,
                UserId = userId,
                Email = email,
                Username = username
            });
        }

        [Route("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string UserId, string code)
        {
            var result = await accountService.ConfirmEmail(UserId, code);

            if (result != null && result.Succeeded)
            {
                return View();
            }

            return BadRequest();
        }

        [Route("EmailVerification")]
        public IActionResult EmailVerification(EmailVerificationViewModel emailVerificationViewModel) => View("EmailVerification", emailVerificationViewModel);

        //POST
        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInModel, string RememberMe)
        {
            if (ModelState.IsValid)
            {               
                var signInDTO = new SignInUserDTO()
                {
                    Username = signInModel.Username,
                    Password = signInModel.Password,
                    RememberMe = RememberMe
                };

                var result = await accountService.SignInAsync(signInDTO);                          

                if (result.SignInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (!result.EmailConfirmed)
                    {
                        var emailVerificationInfoDTO = await accountService.GetEmailVerificationInfo(signInModel.Username);
                        var emailVerificationViewModel = new EmailVerificationViewModel()
                        {
                            VerificationCode = emailVerificationInfoDTO.VerificationCode,
                            UserId = emailVerificationInfoDTO.UserId,
                            Username = emailVerificationInfoDTO.Username,
                            Email = emailVerificationInfoDTO.Email
                        };

                        return View("EmailVerification", emailVerificationViewModel);
                    }
                    ModelState.AddModelError("", "Username or password are wrong. Please try again");
                }

            }
            
            return View("SignIn", signInModel);
        }

        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await accountService.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
