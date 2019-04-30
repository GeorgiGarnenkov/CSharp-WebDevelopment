using System.Linq;
using Chushka.Data.Models;
using Chushka.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Chushka.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace Chushka.Controllers
{
    public class UsersController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public IActionResult Login()
        {
            this.ViewData["Title"] = "Login";

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                this.ViewBag.LoginError = "Wrong username or password !";
                //this.ModelState.AddModelError("LoginError", "Invalid Login attempt");
                return this.Login();
            }

            return this.Login();
        }

        public IActionResult Register()
        {
            this.ViewData["Title"] = "Register";

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (this.ModelState.IsValid && viewModel.Password == viewModel.ConfirmPassword)
            {
                var userExists = this.Db
                                        .Users
                                        .FirstOrDefault(x => x.UserName == viewModel.Username) != null;

                if (userExists)
                {
                    return this.Register();
                }

                var user = new User()
                {
                    UserName = viewModel.Username,
                    Email = viewModel.Email,
                    FullName = viewModel.FullName
                };

                var roleName = "User";
                if (!EnumerableExtensions.Any(this.Db.Users))
                {
                    roleName = "Admin";
                }

                var result = await this._userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    if (!await this._roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new IdentityRole(roleName);
                        await this._roleManager.CreateAsync(role);
                    }

                    await this._userManager.AddToRoleAsync(user, roleName);
                    await this._signInManager.SignInAsync(user, isPersistent: true);
                    return this.RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.Register();
        }

        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
