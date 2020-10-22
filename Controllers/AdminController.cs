using CourseProject.Models;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    // makes it so only Administrators have access to the Admin Controller 
    [Authorize(Roles = "Administrator")]


    public class AdminController : Controller
    {
        ApplicationDbContext db;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public AdminController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        // views all roles in database
        public IActionResult AllRole()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        // directs to the AddRole View
        public IActionResult AddRole()
        {
            return View();
        }
        // makes the AddRole page save role creation
        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AllRole");
            }
            return View();
        }

        public async Task<IActionResult> AddUserRole(string id)
        {
            var roleDisplay = db.Roles.Select(x => new
            {
                Id = x.Id,
                Value = x.Name
            }).ToList();
            AdminAddUserRoleViewModel vm = new AdminAddUserRoleViewModel();
            var user = await userManager.FindByIdAsync(id);
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");
            return View(vm);
        }

        // saves the assignment of a role to a user to the database
        [HttpPost]
        public async Task<IActionResult> AddUserRole
            (AdminAddUserRoleViewModel vm)
        {
            var user = await userManager.FindByIdAsync(vm.User.Id);
            var role = await roleManager.FindByIdAsync(vm.Role);
            var result = await userManager.
                AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Alluser", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code,
                    error.Description);
            }
            var roleDisplay = db.Roles.Select(x => new
            {
                Id = x.Id,
                Value = x.Name
            }).ToList();
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");
            return View(vm);
        }

        // directs to the AddLesson View
        public IActionResult AddLesson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            db.Add(lesson);
            await db.SaveChangesAsync();
            return RedirectToAction("AllLesson", "Admin");
        }

        // directs to the AllLesson View
        public IActionResult AllLesson()
        {
            return View(db.Lessons);
        }
        public IActionResult EditLesson(int id)
        {
            Lesson lesson;
            lesson = db.Lessons.Find(id);
            return View(lesson);
        }
        [HttpPost]
        public IActionResult EditLesson(Lesson lesson)
        {
            db.Update(lesson);
            db.SaveChanges();
            return RedirectToAction("AllLesson");
        }
        public IActionResult DeleteLesson(int id)
        {
            Lesson lesson;
            lesson = db.Lessons.Find(id);
            return View(lesson);
        }
        [HttpPost]
        public IActionResult DeleteLesson(Lesson lesson)
        {
            db.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("AllLesson");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
