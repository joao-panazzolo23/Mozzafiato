using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mozzafiato.ViewModels;

namespace Mozzafiato.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await userManager.FindByNameAsync(loginVM.username);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginVM.password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return View(loginVM.ReturnUrl);
                }

            }
            ModelState.AddModelError("", "Falha ao realizar o login.");
            return View(loginVM);
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            var user = new IdentityUser { UserName = registroVM.username };
            var result = await userManager.CreateAsync(user, registroVM.password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");

            }
            return View(registroVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
