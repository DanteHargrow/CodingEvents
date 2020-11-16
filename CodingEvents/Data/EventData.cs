using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Models;

namespace CodingEvents.Data
{
    public class EventData
    {
        //Store events
        static private Dictionary<int, Event> Events = new Dictionary<int, Event>();

        //Add events
        public static void Add(Event newEvent)
        {
            Events.Add(newEvent.Id, newEvent);
        }
        //retrieve events
        public static IEnumerable<Event> GetAll()
        {
            return Events.Values;
        }
        //retrieve single event
        public static Event GetById(int id)
        {
            return Events[id];
        }
        //delete events
        public static void Remove(int id)
        {
            Events.Remove(id);
        }
    }
}
