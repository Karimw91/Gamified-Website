using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DBM3.Models;

public class AdminController : Controller
{
    private readonly Milestonee2Context _context;

    public AdminController(Milestonee2Context context)
    {
        _context = context;
    }

    // Remove an instructor
    [HttpPost]
    public IActionResult RemoveInstructor(int id)
    {
        var instructor = _context.Instructors.Find(id);
        if (instructor == null)
        {
            return NotFound("Instructor not found.");
        }
        _context.Instructors.Remove(instructor);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
