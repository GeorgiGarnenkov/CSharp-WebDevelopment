using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Eventures.Data;
using Eventures.Models;
using Eventures.ViewModels;
using Eventures.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<EventuresUser> signInManager;
        private readonly UserManager<EventuresUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly EventuresDbContext db;
        private readonly IMapper mapper;

        public UsersController(
            SignInManager<EventuresUser> signInManager, UserManager<EventuresUser> userManager, RoleManager<IdentityRole> roleManager, EventuresDbContext dbContext, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = dbContext;
            this.mapper = mapper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError(string.Empty, "You are already signed in.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager
                    .PasswordSignInAsync(viewModel.Username, viewModel.Password, isPersistent: viewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Wrong username or password!");
            }

            return View(viewModel);
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError(string.Empty, "You are already signed in.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var usersExists = await this.db
                    .Users
                    .FirstOrDefaultAsync(x => x.UserName == viewModel.Username) != null;
                if (usersExists)
                {
                    ModelState.AddModelError(string.Empty, $"User \"{viewModel.Username}\" already exists!");
                    return View(viewModel);
                }
                var emailExists = await this.db
                    .Users
                    .FirstOrDefaultAsync(x => x.Email == viewModel.Email) != null;
                if (emailExists)
                {
                    ModelState.AddModelError(string.Empty, $"Email \"{viewModel.Email}\" is already taken!");
                    return View(viewModel);
                }

                var user = this.mapper.Map<RegisterViewModel, EventuresUser>(viewModel);

                var roleName = "User";
                if (!await this.db.Users.AnyAsync())
                {
                    roleName = "Admin";
                }

                var result = await this.userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    //// app.UseSeedRoles() middleware creates Roles User and Admin
                    //if (!await this._roleManager.RoleExistsAsync(roleName))
                    //{
                    //    await this._roleManager.CreateAsync(new IdentityRole(roleName));
                    //}

                    await this.userManager.AddToRoleAsync(user, roleName);
                    await this.signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            var externalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var username = info.Principal.FindFirstValue(ClaimTypes.Surname);
                var user = new EventuresUser { UserName = username, Email = email };

                var roleName = "User";
                if (!await this.db.Users.AnyAsync())
                {
                    roleName = "Admin";
                }

                var registrationResult = await userManager.CreateAsync(user);
                if (registrationResult.Succeeded)
                {
                    var loginResult = await userManager.AddLoginAsync(user, info);
                    if (loginResult.Succeeded)
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        await this.userManager.AddToRoleAsync(user, roleName);
                        return RedirectToAction("Index", "Home");
                    }
                }

                foreach (var error in registrationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult FaceBookLogin(string provider, string returnUrl = null)
        {
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("ExternalLoginCallback", "Users"));
            return Challenge(properties, provider);
        }
    }
}