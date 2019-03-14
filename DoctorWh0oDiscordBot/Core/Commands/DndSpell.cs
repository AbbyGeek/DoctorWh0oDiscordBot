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

namespace DoctorWh0oDiscordBot.Core.Commands
{
    public class DndSpell : ModuleBase<SocketCommandContext>
    {
        //
        // make array of all spell names
        // if command phrase exists in array
        //use that command as trigger word.

        [Command("aid")]
        public async Task AidSpell()
        {
            const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

            var spellInfo = new List<spell>();
            HttpWebRequest request = WebRequest.CreateHttp($"http://dnd5eapi.co/api/spells/?name=Aid");
            request.UserAgent = UserAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var serializer = new JsonSerializer();
                using (StreamReader data = new StreamReader(response.GetResponseStream()))
                using (var jsonReader = new JsonTextReader(data))
                {
                    spellInfo = serializer.Deserialize<List<spell>>(jsonReader); //program exits this command after executing this line.... wtf.
                    string spellURL = spellInfo[0].SpellURL;
                }
                Console.WriteLine("checking in");
            }

            
            await Context.Channel.SendMessageAsync(@"Working");
        }

        public class HelloWorld : ModuleBase<SocketCommandContext>
        {
            [Command("ping")]
            public async Task greeting()
            {
                await Context.Channel.SendMessageAsync("pong");
            }
        }
    }
}
