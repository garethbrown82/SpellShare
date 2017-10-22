using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpellShare.Models;
using SpellShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellShare.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<SpellShareUser> _userManager;
        private readonly SignInManager<SpellShareUser> _signInManager;

        public AccountController(UserManager<SpellShareUser> userManager, SignInManager<SpellShareUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                SpellShareUser newUser = new SpellShareUser();
                newUser.UserName = model.UserName;
                newUser.Email = model.Email;
                newUser.FirstName = model.FirstName;
                newUser.LastName = model.LastName;

                var createResult = await _userManager.CreateAsync(newUser, model.Password);

                if(createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                                        model.UserName,
                                        model.Password,
                                        model.RememberMe,
                                        false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
