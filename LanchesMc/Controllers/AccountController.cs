using AspNetCoreHero.ToastNotification.Abstractions;
using LanchesMc.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IToastifyService _notifyService { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IToastifyService notifyService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notifyService = notifyService;
        }
        
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            });

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);
            
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return View("Index", "Home");
                    }

                    return Redirect(loginVM.ReturnUrl);
                }
                    
            }
            ModelState.AddModelError("", "Falha ao realizar o login!");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registoVM)
        {
            if (ModelState.IsValid)
            {
                var userRegister = new IdentityUser {UserName = registoVM.UserName};
                var result = await _userManager.CreateAsync(userRegister, registoVM.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(userRegister, isPersistent: false);
                    _notifyService.Success("Registrado com sucesso :) ");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registar o usuário");
                    _notifyService.Error("Erro ao registrar :( ");
                }
            }

            return View(registoVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            _notifyService.Information("Logout, ate breve :)");
            return RedirectToAction("Index", "Home");
        }
    }
}
