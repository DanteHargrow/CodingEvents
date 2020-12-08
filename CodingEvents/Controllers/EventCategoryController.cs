using CodingEvents.Data;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers
{
    public class EventCategoryController : Controller
    {
        private EventDbContext context;
        public EventCategoryController(EventDbContext dbContext)
        {
            context = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<EventCategory> eventsCategories = context.EventCategories.ToList();
            return View(eventsCategories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new AddEventCategoryViewModel();
            return View(addEventCategoryViewModel);
        }
        [HttpPost]
        [Route("EventCategory/Create/")]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory eventCategory = new EventCategory
                {
                    Name = addEventCategoryViewModel.Name
                };

                context.EventCategories.Add(eventCategory);
                context.SaveChanges();
                return Redirect("/EventCategory/");
            }
            return View(addEventCategoryViewModel);
        }
    }
}
