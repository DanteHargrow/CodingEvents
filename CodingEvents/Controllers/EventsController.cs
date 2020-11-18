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
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());
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
                EventData.Add(newEvent);
                return Redirect("/Events");
            }
            return View(AddEventViewModel);
        }
        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }
            return Redirect("/Events");
        }
    }
}
