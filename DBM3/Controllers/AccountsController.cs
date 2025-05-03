using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBM3.Models;
using Microsoft.AspNetCore.Authentication;

namespace DBM3.Controllers
{

    public class AccountsController : Controller
    {
        // GET: /Accounts/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Accounts/Register
        [HttpPost]
        public async Task<IActionResult> Register(string Role, string FirstName, string LastName, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingLearner = await _context.Learners.FirstOrDefaultAsync(l => l.Email == Email);
                var existingInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.Email == Email);
                var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == Email);

                if (existingLearner != null || existingInstructor != null || existingAdmin != null)
                {
                    ModelState.AddModelError("", "A user with this email already exists.");
                    return View();
                }

                // Role-based registration
                if (Role == "Learner")
                {
                    var learner = new Learner
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        Password = Password
                    };
                    _context.Learners.Add(learner);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Profile", "Learners", new { id = learner.LearnerId });
                }
                else if (Role == "Instructor")
                {
                    var instructor = new Instructor
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        Password = Password
                    };
                    _context.Instructors.Add(instructor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Dashboard", "Instructors", new { id = instructor.InstructorId });
                }
                else if (Role == "Admin")
                {
                    var admin = new Admin
                    {
                        Name = FirstName + " " + LastName,
                        Email = Email,
                        Password = Password
                    };
                    _context.Admins.Add(admin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Dashboard", "Admins");
                }
            }

            return View();
        }
        // GET: /Accounts/Login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            // Clear any session data or authentication tokens
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // POST: /Accounts/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Please enter both email and password.");
                return View();
            }

            // Check Learner
            var learner = await _context.Learners.FirstOrDefaultAsync(l => l.Email == email && l.Password == password);
            if (learner != null)
            {
                return RedirectToAction("Profile", "Learners", new { id = learner.LearnerId });
            }

            // Check Instructor
            var instructor = await _context.Instructors.FirstOrDefaultAsync(i => i.Email == email && i.Password == password);
            if (instructor != null)
            {
                return RedirectToAction("Dashboard", "Instructors", new { id = instructor.InstructorId });
            }

            // Check Admin
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
            if (admin != null)
            {
                return RedirectToAction("Dashboard", "Admins");
            }

            ModelState.AddModelError("", "Invalid login credentials.");
            return View();
        }
        private readonly Milestonee2Context _context;

        public AccountsController(Milestonee2Context context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Account.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,Password,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,Password,Role")] Account account)
        {
            if (id != account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.ID))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.ID == id);
        }
    }

}
