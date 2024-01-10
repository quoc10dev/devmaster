using CMS.Interface;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    public class AppController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public AppController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [Authorize]
        public IActionResult ChangePass()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePass changed)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal claims = this.User;
                var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _unitofWork.User.ChangePass(changed, userId);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }

                }               
            }
            return View(changed);
            
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogIn login)
        {
            if(ModelState.IsValid)
            {
                var result = await _unitofWork.User.ValidateUser(login);
                if (result.Item1)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu!");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _unitofWork.User.Logout();
            return RedirectToAction(nameof(LogIn));
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp signup)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitofWork.User.CreateNewUser(signup);
                if (result.Item1)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach(var err in result.Item2)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(signup);
        }

    }
}
