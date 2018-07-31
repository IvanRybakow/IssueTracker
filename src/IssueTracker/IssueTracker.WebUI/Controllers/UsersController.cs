using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IssueTracker.Common.Models;
using IssueTracker.Persistance;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IssueTrackerContext _context;

        public UsersController(IssueTrackerContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Where(u => !u.IsDeleted).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,FirstName,LastName,Password")] UserEntity userEntity)
        {
            if (await _context.Users.AnyAsync(u => u.Login == userEntity.Login))
            {
                ModelState.AddModelError("Login", "This Login is already in use, please pick another.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(userEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userEntity);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (userEntity == null)
            {
                return NotFound();
            }
            return View(userEntity);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,FirstName,LastName,Password")] UserEntity userEntity)
        {
            if (id != userEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEntityExists(userEntity.Id))
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
            return View(userEntity);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (userEntity != null)
            {
                userEntity.IsDeleted = true;
                _context.Users.Update(userEntity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string password)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(m => m.Login == login && m.Password == password);
            if (userEntity != null)
            {
                await Authenticate(userEntity.Login);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}