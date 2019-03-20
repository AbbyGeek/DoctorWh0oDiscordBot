using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using DoctorWh0oDiscordBot.Models;
using System.Linq;

namespace DoctorWh0oDiscordBot.Core.Commands
{
    
    public class DndSpell : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";
        string[] SpellArray;
        

        [Command("spells")]
        public async Task produceURL(string message)
        {
            message = message.ToLower();
            SpellArray = Context.Message.ToString().Split(" ");


            Dictionary<string, string> SpellDictionary = CreateArrayOfAllSpells(SpellArray);
            string url;
            if (SpellDictionary.TryGetValue(message.ToString(), out url))
            {
                int spellIndex = (Array.IndexOf(SpellDictionary.Keys.ToArray(), message) + 1);

                SpellDetails spellDetials = ProduceSpellInfo(spellIndex);
                EmbedBuilder SpellCard = SpellCardMaker(spellDetials);
                await Context.Channel.SendMessageAsync("", false, SpellCard.Build());
            }

        }


        public Dictionary<string, string> CreateArrayOfAllSpells(string[] SpellArray)
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

        public SpellDetails ProduceSpellInfo(int spellIndex)
        {
            using (var webClient = new WebClient())
            {
                string rawJSON = webClient.DownloadString("http://dnd5eapi.co/api/spells/" + spellIndex);
                
                SpellDetails spellInfo = JsonConvert.DeserializeObject<SpellDetails>(rawJSON);

                return(spellInfo);
            }
            
        }

        public EmbedBuilder SpellCardMaker(SpellDetails spellDetails)
        {
            string components ="";
            foreach (string x in spellDetails.components)
            {
                components += x + " ";
            }

            var spellCard = new EmbedBuilder();
            spellCard.WithTitle(spellDetails.name);

            spellCard.AddField("Casting Time", spellDetails.casting_time, true);
            spellCard.AddField("Range", spellDetails.range, true);
            spellCard.AddField("Duration", spellDetails.duration, true);
            spellCard.AddField("Components", components, true);
            spellCard.AddField("Materials", spellDetails.material, true);

            spellCard.WithDescription(spellDetails.desc[0]);


            return spellCard;
        }
    }
    
}

