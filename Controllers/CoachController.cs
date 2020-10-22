using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    //[Authorize(Roles = "Coach, Administrator")]
    public class CoachController : Controller
    {
        ApplicationDbContext db;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public CoachController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProfile()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Coach coach = new Coach();
            if (db.Coaches.Any(i => i.UserId == currentUserId))
            {
                coach = db.Coaches.FirstOrDefault
                    (i => i.UserId == currentUserId);
            }
            else
            {
                coach.UserId = currentUserId;
            }
            return View(coach);
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile
            (Coach coach)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (db.Coaches.Any(i => i.UserId == currentUserId))
            {
                var coachToUpdate = db.Coaches.FirstOrDefault
                    (i => i.UserId == currentUserId);
                coachToUpdate.CoachName = coach.CoachName;
                coachToUpdate.CoachEmail = coach.CoachEmail;
                coachToUpdate.CoachPhone = coach.CoachPhone;
                db.Update(coachToUpdate);
            }
            else
            {
                db.Add(coach);
            }
            await db.SaveChangesAsync();
            return View("Index");
        }

        public IActionResult AllLesson()
        {
            return View(db.Lessons);
        }

        public IActionResult AddSession()
        {
            Session session = new Session();
            var currentUserId = this.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
            session.CoachId = db.Coaches.SingleOrDefault(i => i.UserId == currentUserId).CoachId;
            return View(session);

        }
        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            db.Add(session);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Coach");
        }
        public async Task<IActionResult> SessionByCoach()
        {
            var currentUserId = this.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
            var CoachId = db.Coaches.SingleOrDefault
                (i => i.UserId == currentUserId).CoachId;
            var session = await db.Sessions.Where(i =>
            i.CoachId == CoachId).ToListAsync();
            return View(session);
        }

        /*
            var postGradeSwimmer = await db.Enrollments.Where
                (e => e.SwimmerId == id).ToListAsync();
            List<Swimmer> gradedswimmer = new List<Swimmer>();
            foreach (var e in postGradeSwimmer)
            {
                var swimmer = await db.Swimmers.
                    SingleOrDefaultAsync
                    (s => s.SwimmerId == e.SwimmerId);
                gradedswimmer.Add(swimmer);
            }
            ViewData["enrollment"] = db.Swimmers.Find(id).SwimmerName;
            return View(gradedswimmer);
        }*/


        public async Task<IActionResult> PostGrade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allSwimmers = await db.Enrollments.Include
                (c => c.Session).Where(c => c.SessionId == id)
                .ToListAsync();
            if (allSwimmers == null)
            {
                return NotFound();
            }
            return View(allSwimmers);
        }
        [HttpPost]
        public IActionResult PostGrade(List<Enrollment> enrollments)
        {
            foreach (var enrollment in enrollments)
            {
                var er = db.Enrollments.Find(enrollment.EnrollmentId);
                er.LetterGrade = enrollment.LetterGrade;
            }
            db.SaveChanges();
            return RedirectToAction("SessionByCoach");
        }
        public IActionResult EditSession(int id)
        {
            Session session;
            session = db.Sessions.Find(id);
            return View(session);
        }
        [HttpPost]
        public IActionResult EditSession(Session session)
        {
            db.Update(session);
            db.SaveChanges();
            return RedirectToAction("SessionByCoach");
        }
        public IActionResult DeleteSession(int id)
        {
            Session session;
            session = db.Sessions.Find(id);
            return View(session);
        }
        [HttpPost]
        public IActionResult DeleteSession(Session session)
        {
            db.Remove(session);
            db.SaveChanges();
            return RedirectToAction("SessionByCoach");
        }
    }
}

