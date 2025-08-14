using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PROG7312_P1_V1.Database;
using PROG7312_P1_V1.DataModels;
using PROG7312_P1_V1.ViewModel;

namespace PROG7312_P1_V1.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;
        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Events()
        {
            // gets announcements from DB
            var announcements = _context.Announcements.OrderByDescending(a => a.DatePosted).ToList();

            // gets events from DB
            var events = _context.Events.OrderByDescending(e => e.DatePosted).ToList();

            // creates a view model to pass data to the view
            var viewModel = new EventsAnnouncementsViewModel
            {
                Announcements = announcements,
                Events = events
            };

            return View(viewModel);
        }

    }
}
