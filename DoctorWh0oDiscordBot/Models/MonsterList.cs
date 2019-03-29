using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWh0oDiscordBot.Models
{
    public class Monster
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class MonsterList
    {
        public int count { get; set; }
        public List<Monster> results { get; set; }
    }
}
