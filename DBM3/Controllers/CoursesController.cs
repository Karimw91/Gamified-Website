using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBM3.Models;
using Microsoft.Data.SqlClient;

namespace DBM3.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Milestonee2Context _context;

        public CoursesController(Milestonee2Context context)
        {
            _context = context;
        }

        // GET: Courses
        // Example Action in CoursesController
        public async Task<IActionResult> Index()
        {
            var coursesWithStatus = await (from course in _context.Courses
                                           join enrollment in _context.CourseEnrollments
                                           on course.CourseId equals enrollment.CourseId into courseGroup
                                           from enrollment in courseGroup.DefaultIfEmpty()
                                           select new
                                           {
                                               course.CourseId,
                                               course.Title,
                                               course.LearningObjective,
                                               course.CreditPoints,
                                               course.DifficultyLevel,
                                               course.PreRequisites,
                                               course.Description,
                                               Status = enrollment.Status ?? "Not Enrolled"
                                           }).ToListAsync();

            // Map to a ViewModel
            var courseList = coursesWithStatus.Select(c => new Course
            {
                CourseId = c.CourseId,
                Title = c.Title,
                LearningObjective = c.LearningObjective,
                CreditPoints = c.CreditPoints,
                DifficultyLevel = c.DifficultyLevel,
                PreRequisites = c.PreRequisites,
                Description = c.Description,
                Status = c.Status
            });

            return View(courseList);
        }


        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Title,LearningObjective,CreditPoints,DifficultyLevel,PreRequisites,Description")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,LearningObjective,CreditPoints,DifficultyLevel,PreRequisites,Description")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Check if the course has any students enrolled
                var isEnrolled = _context.CourseEnrollments.Any(e => e.CourseId == id);

                if (isEnrolled)
                {
                    TempData["DeleteError"] = "Cannot delete this course because students are currently enrolled.";
                    return RedirectToAction(nameof(Index));
                }

                // Call the stored procedure to delete the course
                await _context.Database.ExecuteSqlRawAsync("EXEC CourseRemove @p0", id);

                TempData["SuccessMessage"] = "Course deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["DeleteError"] = $"An error occurred while deleting the course: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> ViewModules(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Course ID.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // SQL parameter for the stored procedure
                var courseIdParam = new SqlParameter("@CourseID", id);

                // Execute the FindModules stored procedure
                var modules = await _context.Modules
                    .FromSqlRaw("EXEC FindModules @CourseID", courseIdParam)
                    .ToListAsync();

                // Check if modules are found
                if (modules == null || !modules.Any())
                {
                    TempData["ErrorMessage"] = "No modules found for this course.";
                    return RedirectToAction(nameof(Index));
                }

                // Pass the course title (optional) and modules to the view
                ViewBag.CourseId = id;
                return View("ViewModules", modules);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
