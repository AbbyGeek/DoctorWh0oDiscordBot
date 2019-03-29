using Discord;
using Discord.Commands;
using DoctorWh0oDiscordBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWh0oDiscordBot.Core.Commands.Monsters
{
    public class SearchAllFor : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

        [Command("MonsterSearch")]
        public async Task listNames(string SearchTerm, string SearchValue)
        {
            List<string> monsters = new List<string>();
            using (var webClient = new WebClient())
            {
                var t = typeof(MonsterDetails);
                var y = t.GetProperty(SearchTerm);


                var validSearch = typeof(MonsterDetails).GetProperty(SearchTerm) != null;

                if(validSearch)
                {
                    string rawJSON = webClient.DownloadString($"https://raw.githubusercontent.com/adrpadua/5e-database/master/5e-SRD-Monsters.json");
                    List<MonsterDetails> MonsterList = JsonConvert.DeserializeObject<List<MonsterDetails>>(rawJSON);
                    y = typeof(MonsterDetails).GetProperty(SearchTerm);
                    List<string> names = new List<string>();

                    foreach (MonsterDetails monster in MonsterList)
                    {
                        if (monster.GetType().GetProperty(SearchTerm).GetValue(monster, null).ToString() == SearchValue)
                        {
                            names.Add(monster.name);
                        }

                    }

                    string finalList = string.Join("\n", names.ToArray());
                    await Context.Channel.SendMessageAsync(finalList);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("does not exist as parameter");
                }


            }
        }
    }
}
