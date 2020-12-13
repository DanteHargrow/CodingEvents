using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Models
{
    public class EventCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Event> events { get; set; }

        public EventCategory()
        {
        }

        public EventCategory(string name):this()
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override bool Equals(object obj)
        {
            return obj is EventCategory category &&
                   Id == category.Id;
        }
    }
}
