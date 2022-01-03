using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using DoctorWh0oDiscordBot.Models;
using System.Linq;

namespace DoctorWh0oDiscordBot.Core.Commands.Spells
{
    public class SpellList : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

        [Command("spelllist"), Alias("SpellList")]
        public async Task produceList()
        {
            Dictionary<string, string> SpellDictionary = CreateArrayOfAllSpells();
            string response = "";

            for (int i = 0; i < SpellDictionary.Count; i++)
            {

                response += $"{SpellDictionary.Keys.ElementAt(i)} \n";
                if (i % 20 == 0)
                {
                    await Context.Channel.SendMessageAsync(response);
                    response = "";
                }

            }
            await Context.Channel.SendMessageAsync(response);
        }


        public Dictionary<string, string> CreateArrayOfAllSpells()
        {
            using (var webClient = new WebClient())
            {
                //get string representation of JSON
                string rawJSON = webClient.DownloadString("http://dnd5eapi.co/api/spells");
                //convert string to RootObjects
                RootObject spellList = JsonConvert.DeserializeObject<RootObject>(rawJSON);
                var SpellDictionary = new Dictionary<string, string>();

                for (int i = 0; i < spellList.count; i++)
                {
                    SpellDictionary.Add(spellList.results[i].name.ToLower(), spellList.results[i].url);
                }
                return SpellDictionary;
            }
        }

        
    }

}
