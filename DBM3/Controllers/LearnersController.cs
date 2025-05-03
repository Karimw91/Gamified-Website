 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBM3.Models;
using System.Text.Json;
using DBM3.Utilities;
using ModelLearningGoal = DBM3.Models.LearningGoal;
using Microsoft.Data.SqlClient;

namespace DBM3.Controllers
{
    public class LearnersController : Controller
    {
        private readonly Milestonee2Context _context;

        public LearnersController(Milestonee2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> EnrolledCourses(int learnerId)
        {
            if (learnerId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Learner ID.";
                return RedirectToAction("Dashboard", new { id = learnerId });
            }

            try
            {
                // Fetch enrolled courses for the learner
                var courses = await _context.CourseEnrollments
                    .Include(e => e.Course)
                    .Where(e => e.LearnerId == learnerId)
                    .Select(e => new
                    {
                        e.Course.CourseId,
                        e.Course.Title,
                        e.Status,
                        e.EnrollmentDate,
                        e.CompletionDate
                    })
                    .ToListAsync();

                // Map to a ViewModel or use the anonymous type
                var enrolledCourses = courses.Select(c => new
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.Title,
                    Status = c.Status,
                    EnrollmentDate = c.EnrollmentDate,
                    CompletionDate = c.CompletionDate
                });

                return View("EnrolledCourses", enrolledCourses);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Dashboard", new { id = learnerId });
            }
        }


        public IActionResult Modules(int courseId)
        {
            if (courseId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Course ID.";
                return RedirectToAction("Dashboard");
            }

            try
            {
                // SQL parameter for the stored procedure
                var courseIdParam = new SqlParameter("@CourseID", courseId);

                // Execute the FindModules stored procedure
                var modules = _context.Modules
                    .FromSqlRaw("EXEC FindModules @CourseID", courseIdParam)
                    .ToList();

                // Pass the CourseId to the view for any additional operations
                ViewBag.CourseId = courseId;

                return View(modules);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Dashboard");
            }
        }


        public async Task<IActionResult> Profile(int id)
        {
            var learner = await _context.Learners
                .Include(l => l.Goals)
                .FirstOrDefaultAsync(l => l.LearnerId == id);

            if (learner == null)
            {
                return NotFound("Learner not found.");
            }

            var goals = learner.Goals.Select(goal => new LearningGoalViewModel
            {
                GoalId = goal.Id,
                Description = goal.Description,
                Status = goal.Status,
                Deadline = goal.Deadline.HasValue ? goal.Deadline.Value.ToDateTime(new TimeOnly()) : DateTime.MinValue
            }).ToList();

            var viewModel = new LearnerProfileViewModel
            {
                Learner = learner,
                Goals = goals
            };

            return View(viewModel);
        }
        public async Task<IActionResult> ViewLearningPaths(int learnerId)
        {
            var learningPaths = await _context.LearningPaths
                .Where(lp => lp.LearnerId == learnerId)
                .Select(lp => new LearningPathViewModel
                {
                    PathId = lp.PathId,
                    CompletionStatus = lp.CompletionStatus,
                    CustomContent = lp.CustomContent,
                    AdaptiveRules = lp.AdaptiveRules
                })
                .ToListAsync();

            ViewBag.LearnerId = learnerId;
            return View(learningPaths);
        }

        [HttpPost]
        public async Task<IActionResult> AddGoal(int learnerId, LearningGoalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var learner = await _context.Learners.FindAsync(learnerId);

                if (learner == null)
                {
                    return NotFound("Learner not found.");
                }

                var newGoal = new Models.LearningGoal
                {
                    Description = model.Description,
                    Status = model.Status,
                    Deadline = DateOnly.FromDateTime(model.Deadline)
                };

                learner.Goals.Add(newGoal);
                await _context.SaveChangesAsync();

                return RedirectToAction("Profile", new { id = learnerId });
            }

            return View(model); // Return the model with validation errors if needed
        }
        [HttpPost]
        public async Task<IActionResult> RemoveGoal(int learnerId, int goalId)
        {
            var goal = await _context.LearningGoals.FindAsync(goalId);

            if (goal != null)
            {
                _context.LearningGoals.Remove(goal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { id = learnerId });
        }
        public async Task<IActionResult> ViewGoals(int learnerId)
        {
            var goals = await _context.LearningGoals
                                      .Where(g => g.Learners.Any(l => l.LearnerId == learnerId))
                                      .Select(g => new LearningGoalViewModel
                                      {
                                          GoalId = g.Id,
                                          Status = g.Status, // Valid property
                                          Deadline = g.Deadline.HasValue ? g.Deadline.Value.ToDateTime(new TimeOnly()) : DateTime.MinValue
                                          // Remove or replace Description if it doesn't exist
                                      })
                                      .ToListAsync();

            ViewBag.LearnerId = learnerId; // Pass LearnerId for creating new goals
            return View(goals);
        }
        public async Task<IActionResult> Dashboard(int id)
        {
            // Fetch learner's data based on the provided ID
            var learner = await _context.Learners
                .Include(l => l.CourseEnrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(l => l.LearnerId == id);

            if (learner == null)
            {
                return NotFound("Learner not found.");
            }

            // Fetch learner-specific data
            var completedCourses = learner.CourseEnrollments.Count(e => e.Status == "Completed");
            var currentCourse = learner.CourseEnrollments.FirstOrDefault(e => e.Status == "In Progress")?.Course?.Title ?? "None";

            // Prepare notifications or fetch them dynamically if stored in the database
            var notifications = await _context.Notifications
                .Where(n => n.Id == id)
                .Select(n => n.Message)
                .ToListAsync();
            {
     

    };


            // Create the view model
            var dashboardModel = new LearnerDashboardViewModel
            {
                CompletedCourses = completedCourses,
                CurrentCourse = currentCourse,
                Notifications = notifications,
                  LearnerId = id
            };

            // Return the view with the model
            return View(dashboardModel);
        }

        public async Task<IActionResult> RemovePersonalizationProfile(int learnerId)
        {
            // Fetch the personalization profile for the learner
            var profiles = _context.PersonalizationProfiles
                .Where(p => p.LearnerId == learnerId);

            if (!profiles.Any())
            {
                return NotFound("No personalization profile found for this learner.");
            }

            // Remove the personalization profile and related data
            _context.PersonalizationProfiles.RemoveRange(profiles);

            // Save changes to the database
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Personalization profile removed successfully.";
            return RedirectToAction("Dashboard", new { id = learnerId });
            TempData["SuccessMessage"] = "Personalization profile removed successfully.";
            TempData["ErrorMessage"] = "An error occurred while removing the profile.";

        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            // Fetch the learner's account using their ID
            var learner = await _context.Learners
                .Include(l => l.PersonalizationProfiles)
                .ThenInclude(p => p.HealthConditions)
                .FirstOrDefaultAsync(l => l.LearnerId == id);

            if (learner == null)
            {
                return NotFound("Learner not found.");
            }

            // Remove associated data (PersonalizationProfiles and HealthConditions)
            if (learner.PersonalizationProfiles.Any())
            {
                _context.PersonalizationProfiles.RemoveRange(learner.PersonalizationProfiles);
            }

            // Remove the learner's account
            _context.Learners.Remove(learner);

            await _context.SaveChangesAsync();

            // Log the user out and redirect to the home page
            return RedirectToAction("Logout", "Accounts");
        }

        // GET: Learners/EditProfile/{id}
        public async Task<IActionResult> EditProfile(int id)
        {
            var learner = await _context.Learners.FindAsync(id);
            if (learner == null)
            {
                return NotFound();
            }
            return View(learner);
        }
        // POST: Learners/EditProfile/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, Learner learner)
        {
            if (id != learner.LearnerId)
            {
                return NotFound("Invalid Learner ID.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch existing learner data
                    var existingLearner = await _context.Learners.FindAsync(id);
                    if (existingLearner == null)
                    {
                        return NotFound("Learner not found.");
                    }

                    // Update only the necessary fields
                    existingLearner.FirstName = learner.FirstName;
                    existingLearner.LastName = learner.LastName;
                    existingLearner.Email = learner.Email;
                    existingLearner.Country = learner.Country;
                    existingLearner.CulturalBackground = learner.CulturalBackground;
                    existingLearner.Gender = learner.Gender;
                    existingLearner.BirthDate = learner.BirthDate;

                    // Save changes to the database
                    _context.Update(existingLearner);
                    await _context.SaveChangesAsync();

                    // Redirect back to the profile page
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("Profile", new { id = existingLearner.LearnerId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the profile: " + ex.Message);
                }
            }

            return View(learner); // Return to the EditProfile view with errors
        }

        public async Task<IActionResult> ViewPersonalizationProfile(int learnerId)
        {
            var profiles = await _context.PersonalizationProfiles
                .Include(p => p.HealthConditions)
                .Where(p => p.LearnerId == learnerId)
                .ToListAsync();

            if (!profiles.Any())
            {
                TempData["WarningMessage"] = "Your personalization profile has been deleted. Please create a new one to access this feature.";
                return RedirectToAction("Dashboard", new { id = learnerId });
            }

            return View(profiles);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Learners.ToListAsync());
        }

        // GET: Learners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = await _context.Learners
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // GET: Learners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Learners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearnerId,FirstName,LastName,Gender,BirthDate,Country,CulturalBackground")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(learner);
        }

        // GET: Learners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = await _context.Learners.FindAsync(id);
            if (learner == null)
            {
                return NotFound();
            }
            return View(learner);
        }

        // POST: Learners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearnerId,FirstName,LastName,Gender,BirthDate,Country,CulturalBackground")] Learner learner)
        {
            if (id != learner.LearnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerId))
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
            return View(learner);
        }

        // GET: Learners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = await _context.Learners
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // POST: Learners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learner = await _context.Learners.FindAsync(id);

            if (learner != null)
            {
                // Remove the learner
                _context.Learners.Remove(learner);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Home page after deletion
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EnrollInCourse(int LearnerId, int CourseId)
        {
            if (LearnerId <= 0 || CourseId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Learner ID or Course ID.";
                return RedirectToAction("Dashboard", new { id = LearnerId });
            }

            try
            {
                // Check if the learner exists
                bool learnerExists = await _context.Learners.AnyAsync(l => l.LearnerId == LearnerId);
                if (!learnerExists)
                {
                    TempData["ErrorMessage"] = "Error: Learner does not exist.";
                    return RedirectToAction("Dashboard", new { id = LearnerId });
                }

                // Check if the course exists
                bool courseExists = await _context.Courses.AnyAsync(c => c.CourseId == CourseId);
                if (!courseExists)
                {
                    TempData["ErrorMessage"] = "Error: Course does not exist.";
                    return RedirectToAction("Dashboard", new { id = LearnerId });
                }

                // Check if the learner is already enrolled
                bool alreadyEnrolled = await _context.CourseEnrollments
                    .AnyAsync(ce => ce.LearnerId == LearnerId && ce.CourseId == CourseId);
                if (alreadyEnrolled)
                {
                    TempData["ErrorMessage"] = "Error: Learner is already enrolled in this course.";
                    return RedirectToAction("Dashboard", new { id = LearnerId });
                }

                // Track the number of enrollments before the procedure runs
                int enrollmentCountBefore = await _context.CourseEnrollments
                    .CountAsync(ce => ce.LearnerId == LearnerId && ce.CourseId == CourseId);

                // Call the stored procedure
                var learnerParam = new SqlParameter("@LearnerID", LearnerId);
                var courseParam = new SqlParameter("@CourseID", CourseId);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC Courseregister @LearnerID, @CourseID",
                    learnerParam,
                    courseParam
                );

                // Track the number of enrollments after the procedure runs
                int enrollmentCountAfter = await _context.CourseEnrollments
                    .CountAsync(ce => ce.LearnerId == LearnerId && ce.CourseId == CourseId);

                // Check if a new enrollment was added
                if (enrollmentCountAfter > enrollmentCountBefore)
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

        private bool LearnerExists(int id)
        {
            return _context.Learners.Any(e => e.LearnerId == id);
        }
    }
}
