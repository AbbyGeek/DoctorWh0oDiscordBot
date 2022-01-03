using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWh0oDiscordBot.Models
{

    public class AreaOfEffect
    {
        public string type { get; set; }
        public int size { get; set; }
    }

    public class School
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Class
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Subclass
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class SpellDetails
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<string> desc { get; set; }
        public string range { get; set; }
        public List<string> components { get; set; }
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public bool concentration { get; set; }
        public string casting_time { get; set; }
        public int level { get; set; }
        public AreaOfEffect area_of_effect { get; set; }
        public School school { get; set; }
        public List<Class> classes { get; set; }
        public List<Subclass> subclasses { get; set; }
        public string url { get; set; }
    }

}
