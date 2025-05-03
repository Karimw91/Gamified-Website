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
    public class InstructorsController : Controller
    {

        private readonly Milestonee2Context _context;

        public InstructorsController(Milestonee2Context context)
        {
            _context = context;
        }

        public IActionResult CreateLearningPath(int instructorId)
        {
            if (instructorId == 0)
            {
                TempData["ErrorMessage"] = "Instructor ID is invalid.";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.InstructorId = instructorId; // Pass the instructor ID to the view
            ViewBag.Learners = _context.Learners.ToList(); // Fetch learners for selection

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLearningPath(LearningPathViewModel model)
        {
            if (ModelState.IsValid)
            {
                var learningPath = new LearningPath
                {
                    LearnerId = model.LearnerId,
                    CompletionStatus = "Not Started",
                    CustomContent = model.CustomContent,
                    AdaptiveRules = model.AdaptiveRules
                };

                _context.LearningPaths.Add(learningPath);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Learning path created successfully!";
                return RedirectToAction("Dashboard", new { id = model.InstructorId });
            }

            ViewBag.Learners = _context.Learners.ToList();
            ViewBag.InstructorId = model.InstructorId;
            return View(model);
        }
        public async Task<IActionResult> EditProfile(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Profile updated successfully.";
                    return RedirectToAction("Dashboard", new { id = instructor.InstructorId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(instructor);
        }
        // View Learning Paths
        public async Task<IActionResult> ViewPaths(int instructorId)
        {
            // Fetch Learning Paths and convert them to the ViewModel
            var paths = await _context.LearningPaths
                                      .Select(p => new LearningPathViewModel
                                      {
                                          PathId = p.PathId,
                                          InstructorId = instructorId,
                                          Description = p.CustomContent ?? "Default Description", // Map CustomContent to Description
                                          CreatedAt = DateTime.Now // Placeholder since CreatedAt isn't in the database
                                      })
                                      .ToListAsync();

            ViewBag.InstructorId = instructorId;
            return View(paths);
        }

        // Add Learning Path
        public IActionResult AddPath(int instructorId)
        {
            ViewBag.InstructorId = instructorId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPath(int instructorId, string description)
        {
            // Add a new LearningPath
            var newPath = new LearningPath
            {
                CustomContent = description, // Use CustomContent as Description
                AdaptiveRules = "Default",   // Example placeholder
                CompletionStatus = "In Progress" // Placeholder
            };

            _context.LearningPaths.Add(newPath);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewPaths", new { instructorId });
        }

        public async Task<IActionResult> Dashboard(int id)
        {
            if (id == 0)
            {
                TempData["ErrorMessage"] = "Instructor ID is missing.";
                return RedirectToAction("Index", "Home");
            }

            // Fetch the instructor along with their courses
            var instructor = await _context.Instructors
                .Include(i => i.Courses) // Assuming there's a navigation property "Courses"
                .FirstOrDefaultAsync(i => i.InstructorId == id);

            if (instructor == null)
            {
                return NotFound("Instructor not found.");
            }

            // Populate the ViewModel
            var viewModel = new InstructorDashboardViewModel
            {
                InstructorId = instructor.InstructorId,
                Name = $"{instructor.FirstName} {instructor.LastName}",
                Email = instructor.Email,
                CoursesTaught = instructor.Courses?.ToList() ?? new List<Course>() // Ensure it's not null
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(course);
        }
        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorId,Name,LatestQualification,ExpertiseArea,Email,Password")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,Name,LatestQualification,ExpertiseArea,Email,Password")] Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorId))
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
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollLearnerInCourse(int LearnerId, int CourseId)
        {
            if (LearnerId <= 0 || CourseId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Learner ID or Course ID.";
                return RedirectToAction("Dashboard", new { id = LearnerId });
            }

            try
            {
                // Check the enrollment count before calling the procedure
                var enrollmentBefore = await _context.CourseEnrollments
                    .CountAsync(e => e.LearnerId == LearnerId && e.CourseId == CourseId);

                // Execute the stored procedure
                var learnerParam = new SqlParameter("@LearnerID", LearnerId);
                var courseParam = new SqlParameter("@CourseID", CourseId);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC Courseregister @LearnerID, @CourseID",
                    learnerParam,
                    courseParam
                );

                // Check the enrollment count after calling the procedure
                var enrollmentAfter = await _context.CourseEnrollments
                    .CountAsync(e => e.LearnerId == LearnerId && e.CourseId == CourseId);

                // Determine if the course enrollment was successful
                if (enrollmentAfter > enrollmentBefore)
                {
                    TempData["SuccessMessage"] = "You have successfully registered for the course.";
                }
                else
                {
                    TempData["ErrorMessage"] = "You cannot register for this course as the prerequisites have not been completed.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction("Dashboard", new { id = LearnerId });
        }



        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.InstructorId == id);
        }
    }

}
