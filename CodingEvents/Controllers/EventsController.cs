using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Models;
using CodingEvents.Data;
using Microsoft.AspNetCore.Mvc;
using CodingEvents.ViewModels;

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

            List<Event> events = context.Events.ToList();
            return View(events);
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel AddEventViewModel = new AddEventViewModel();
            return View(AddEventViewModel);
        }
        [HttpPost]
        [Route("Events/Add/")]
        public IActionResult Add(AddEventViewModel AddEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = AddEventViewModel.Name,
                    Description = AddEventViewModel.Description,
                    ContactEmail = AddEventViewModel.ContactEmail,
                    Type = AddEventViewModel.Type
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
    }
}
