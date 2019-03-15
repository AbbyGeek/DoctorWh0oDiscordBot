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

namespace DoctorWh0oDiscordBot.Core.Commands
{
    public class DndSpell : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";
       
        



        // make array of all spell names
        // if command phrase exists in array
        //use that command as trigger word.

        [Command("aid")]
        public async Task AidSpell()
        {
            using (var webClient = new WebClient())
            {
                //get string representation of JSON
                string rawJSON = webClient.DownloadString("http://dnd5eapi.co/api/spells");
                //convert string to RootObjects
                RootObject spells = JsonConvert.DeserializeObject<RootObject>(rawJSON);
                await Context.Channel.SendMessageAsync(spells.results[0].name);
                await Context.Channel.SendMessageAsync(spells.results[0].url);


            }
        }
    }
}

