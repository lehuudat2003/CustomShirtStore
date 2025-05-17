using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.InteropServices;
using System.Text;

namespace CustomShirtStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager, RoleManager<Role> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không đúng.");
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserAccount user = new UserAccount()
            {
                UserName = model.Email,
                Email = model.Email,
                Password = "",
                Provider = "",
                ProviderId = "",
                //2: Customer
                RoleId = 2,
                PhoneNumber = model.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email không tồn tại hoặc không hợp lệ.");
                    return View(model);
                }

                if (user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email đã được xác nhận.");
                    return View(model);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var confirmationLink = Url.Action("ChangePassword", "Account", new {username = user.UserName, userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email, "Xác nhận địa chỉ email",
        $"Vui lòng xác nhận email bằng cách nhấn vào liên kết sau: <a href='{confirmationLink}'>Xác nhận email</a>");
                ViewData["Message"] = "Một liên kết xác nhận đã được gửi đến email của bạn. Vui lòng kiểm tra hộp thư đến của bạn.";
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword(string userId, string token,string username)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
                return RedirectToAction("VerifyEmail", "Account");
            return View(new ChangePasswordViewModel { Email = username,Token = token });
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email không hợp lệ.");
                }
            }
            return View(model);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
