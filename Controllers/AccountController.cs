using CourseProject.Models;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
        }
        // register view page
        public IActionResult Register()
        {
            return View();
        }
        // registers the new user's information in the database
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email
                };
                // adds a new user with the password to the database
                var result = await userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    // signs in new user without using persistent cookie. When 
                    // browser is closed, user is auto-logged out.
                    await signInManager.SignInAsync(user, false);
                    // forwards newly registered user to Index
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }
        // login view page
        public IActionResult Login()
        {
            return View();
        }
        // confirms username and password, then allows user to login
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync
                    (vm.Email, vm.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(vm.Email);
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles.Contains("Coach"))
                    {
                        return RedirectToAction("Index", "Coach");
                    }
                    if (roles.Contains("Administrator"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roles.Contains("Swimmer"))
                    {
                        return RedirectToAction("Index", "Swimmer");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login Failure.");
            }
            return View(vm);
        }
        // list all users. For each user, there will be a link to add a role for that user
        public IActionResult AllUser()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // logout method
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }



        // end of controller
    }
}
