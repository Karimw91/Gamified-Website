using DBM3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProfileController : Controller
{
    private readonly Milestonee2Context _context;

    public ProfileController(Milestonee2Context context)
    {
        _context = context;
    }

    // View Profile
    public IActionResult ViewProfile(int id, string role)
    {
        if (role == "Learner")
        {
            // Call the stored procedure to get learner data by ID
            var learner = _context.Learners.FromSqlRaw("EXEC ViewInfo @LearnerID = {0}", id).FirstOrDefault();

            if (learner == null)
            {
                return NotFound("Learner not found.");
            }

            return View("LearnerProfile", learner);
        }
        else if (role == "Instructor")
        {
            var instructor = _context.Instructors.Find(id);
            return View("InstructorProfile", instructor);
        }
        return NotFound("Role not recognized.");
    }

    // Edit Profile (GET)
    [HttpGet]
    public IActionResult EditProfile(int id, string role)
    {
        if (role == "Learner")
        {
            var learner = _context.Learners.Include(l => l.PersonalizationProfiles)
                                           .FirstOrDefault(l => l.LearnerId == id);
            if (learner == null)
            {
                return NotFound("Learner not found.");
            }

            return View("EditLearnerProfile", learner);
        }
        else if (role == "Instructor")
        {
            var instructor = _context.Instructors.Find(id);
            return View("EditInstructorProfile", instructor);
        }
        return NotFound("Role not recognized.");
    }
    // Edit Profile (POST) - Update Profile using the stored procedure
    [HttpPost]
    public IActionResult EditProfile(int learnerId, int profileId, string preferredContentType, string emotionalState, string personalityType)
    {
        // Call the stored procedure to update the profile details
        _context.Database.ExecuteSqlRaw("EXEC ProfileUpdate @LearnerID = {0}, @ProfileID = {1}, @Preferred_content_type = {2}, @emotional_state = {3}, @personality_type = {4}",
                                        learnerId, profileId, preferredContentType, emotionalState, personalityType);

        // After the profile is updated, redirect to the view profile page
        return RedirectToAction("ViewProfile", new { id = learnerId, role = "Learner" });
    }
}
