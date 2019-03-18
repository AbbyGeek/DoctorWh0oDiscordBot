using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using DoctorWh0oDiscordBot.Models;
using Newtonsoft.Json.Linq;
using System.Threading;
using Discord.WebSocket;

namespace DoctorWh0oDiscordBot.Core.Commands
{
    
    public class DndSpell : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";
        string[] SpellArray;
        

        [Command("spells")]
        public async Task produceURL(string message)
        {
            SpellArray = Context.Message.ToString().Split(" ");


            Dictionary<string, string> SpellDictionary = spells(SpellArray);
            string url;

            if (SpellDictionary.TryGetValue(message.ToString(), out url))
            {
                await Context.Channel.SendMessageAsync(url);
            }

        }


        public Dictionary<string, string> spells(string[] SpellArray)
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
                    SpellDictionary.Add(spellList.results[i].name, spellList.results[i].url);
                }
                return SpellDictionary;
            }
        }
    }
    
}

