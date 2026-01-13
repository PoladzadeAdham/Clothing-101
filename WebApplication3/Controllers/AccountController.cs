using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.ViewModel.UserViewModel;

namespace WebApplication3.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");

        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin",
            });

            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "member"
            });

            return Ok("Ok role created");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) 
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if(user == null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user,vm.Password,false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]      
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if(!ModelState.IsValid)
                return View(vm);

            AppUser user = new()
            {
                UserName = vm.UserName,
                Email = vm.Email,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(vm);
                }
            }

            await _userManager.AddToRoleAsync(user, "member");

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");

        }


    }
}
