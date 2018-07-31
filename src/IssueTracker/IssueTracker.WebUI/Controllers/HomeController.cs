using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using IssueTracker.WebUI.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IssueTrackerContext _context;

        public HomeController(IssueTrackerContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            var recentActivities = await _context.IssueTransitions
                .Include(i => i.MadeBy)
                .Include(i => i.Issue)
                .OrderByDescending(i => i.TransitionDate)
                .Take(5).ToListAsync();
            var lastIssues = await _context.Issues
                .Include(i => i.CreatedBy)
                .OrderByDescending(i => i.CreationDate)
                .Take(5).ToListAsync();
            var model = new DashboardViewModel
            {
                LastIssues = lastIssues,
                RecentActivities = recentActivities
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
