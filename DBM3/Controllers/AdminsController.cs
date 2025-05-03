using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBM3.Models;

namespace DBM3.Controllers
{
    public class AdminsController : Controller
    {
        private readonly Milestonee2Context _context;

        public AdminsController(Milestonee2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var learners = await _context.Learners.ToListAsync(); // Fetch all learners
            var totalLearners = learners.Count;
            var totalInstructors = await _context.Instructors.CountAsync();
            var totalCourses = await _context.Courses.CountAsync();

            var model = new AdminDashboardViewModel
            {
                TotalLearners = totalLearners,
                TotalInstructors = totalInstructors,
                TotalCourses = totalCourses,
                Learners = learners
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemovePersonalizationProfile(int learnerId)
        {
            var profiles = _context.PersonalizationProfiles.Where(p => p.LearnerId == learnerId);

            if (!profiles.Any())
            {
                TempData["ErrorMessage"] = "No personalization profile found for this learner.";
                return RedirectToAction("Dashboard", "Admins");
            }

            _context.PersonalizationProfiles.RemoveRange(profiles);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Personalization profile removed successfully.";
            return RedirectToAction("Dashboard", "Admins");
        }
        public async Task<IActionResult> ViewPersonalizationProfile(int learnerId)
        {
            var profiles = await _context.PersonalizationProfiles
                .Include(p => p.HealthConditions)
                .Where(p => p.LearnerId == learnerId)
                .ToListAsync();

            if (!profiles.Any())
            {
                TempData["WarningMessage"] = "The learner's personalization profile has been deleted. No data to display.";
                return RedirectToAction("Dashboard");
            }

            return View(profiles);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                TempData["ErrorMessage"] = "Instructor not found.";
                return RedirectToAction("ManageUsers");
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Instructor account removed successfully.";
            return RedirectToAction("ManageUsers");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id, string role)
        {
            if (role == "Learner")
            {
                var learner = await _context.Learners.Include(l => l.PersonalizationProfiles).FirstOrDefaultAsync(l => l.LearnerId == id);
                if (learner != null)
                {
                    _context.Learners.Remove(learner);
                }
                else
                {
                    TempData["ErrorMessage"] = "Learner not found.";
                    return RedirectToAction("ManageUsers");
                }
            }
            else if (role == "Instructor")
            {
                var instructor = await _context.Instructors.FindAsync(id);
                if (instructor != null)
                {
                    _context.Instructors.Remove(instructor);
                }
                else
                {
                    TempData["ErrorMessage"] = "Instructor not found.";
                    return RedirectToAction("ManageUsers");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid role specified.";
                return RedirectToAction("ManageUsers");
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"{role} account deleted successfully.";
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> ManageUsers()
    {
        var learners = await _context.Learners.ToListAsync();
        var instructors = await _context.Instructors.ToListAsync();

        return View(new ManageUsersViewModel
        {
            Learners = learners,
            Instructors = instructors
        });
    }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,Name,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,Name,Email,Password")] Admin admin)
        {
            if (id != admin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
