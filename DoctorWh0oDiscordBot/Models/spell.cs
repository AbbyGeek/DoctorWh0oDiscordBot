using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWh0oDiscordBot.Models
{
    public class Spell
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public List<Spell> results { get; set; }
    }
}
