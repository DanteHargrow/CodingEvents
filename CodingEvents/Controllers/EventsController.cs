using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Models;
using CodingEvents.Data;
using Microsoft.AspNetCore.Mvc;
using CodingEvents.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;
        public EventsController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {

            List<Event> events = context.Events.Include(e => e.Category).ToList();
            return View(events);
        }
        [HttpGet]
        public IActionResult Add()
        {
            List<EventCategory> categories = context.EventCategories.ToList();
            AddEventViewModel AddEventViewModel = new AddEventViewModel(categories);
            return View(AddEventViewModel);
        }
        [HttpPost]
        [Route("Events/Add/")]
        public IActionResult Add(AddEventViewModel AddEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory category = context.EventCategories.Find(AddEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = AddEventViewModel.Name,
                    Description = AddEventViewModel.Description,
                    ContactEmail = AddEventViewModel.ContactEmail,
                    Category = category
                };
                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }
            return View(AddEventViewModel);
        }
        public IActionResult Delete()
        {
            ViewBag.events = context.Events.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int eventId in eventIds)
            {
                Event theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }
            context.SaveChanges();
            return Redirect("/Events");
        }
        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events
                .Include(e => e.Category)
                .Single(e => e.Id == id);
            List<EventTag> eventTags = context.eventTags
                .Where(et => et.EventId == id)
                .Include(et => et.Tag)
                .ToList();

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent, eventTags);

            return View(viewModel);
        }
    }
}
