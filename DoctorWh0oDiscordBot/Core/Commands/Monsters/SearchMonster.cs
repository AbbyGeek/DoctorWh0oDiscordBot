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
    public class SearchMonster : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";
        string MonsterName;

        [Command("monsters")]
        public async Task produceURL(string message)
        {
            MonsterName = message.ToLower();

            Dictionary<string, string> MonsterDictionary = CreateArrayOfAllMonsters();
            string url;
            if(MonsterDictionary.TryGetValue(MonsterName, out url))
            {
                int index = (Array.IndexOf(MonsterDictionary.Keys.ToArray(), MonsterName) + 1);

                MonsterDetails mosnterDetails = ProduceInfo(index);
                EmbedBuilder MonsterCard = MonsterCardMaker(mosnterDetails);
                EmbedBuilder MonsterAction = MonsterActionBuilder(mosnterDetails);
                await Context.Channel.SendMessageAsync("", false, MonsterCard.Build());
                await Context.Channel.SendMessageAsync("", false, MonsterAction.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync(TypoResponse.TypoRespond(MonsterDictionary, MonsterName, "monster"));
            }
            
        }

        private EmbedBuilder MonsterCardMaker(MonsterDetails monsterDetails)
        {
            var monsterCard = new EmbedBuilder();
            monsterCard.WithTitle(monsterDetails.name);



            monsterCard.WithColor(Color.Red);
            monsterCard.AddField("\u200b",$"{monsterDetails.size} {monsterDetails.type} {monsterDetails.subtype}", true);
            monsterCard.AddField("\u200b", monsterDetails.alignment, true);

            monsterCard.AddField("—————————————————————————————————","\u200b", false);

            if (!(monsterDetails.armor_class.ToString() == null)) monsterCard.AddField("Armor Class", monsterDetails.armor_class, true);
            if (!(monsterDetails.hit_points.ToString() == null)) monsterCard.AddField("Hit Points", monsterDetails.hit_points, true);
            if (!(monsterDetails.speed.ToString() == null)) monsterCard.AddField("Speed", monsterDetails.speed, true);

            monsterCard.AddField("—————————————————————————————————", "\u200b", false);

            if (!(monsterDetails.strength.ToString() == null)) monsterCard.AddField("Strength", monsterDetails.strength, true);
            if (!(monsterDetails.dexterity.ToString() == null)) monsterCard.AddField("Dexterity", monsterDetails.dexterity, true);
            if (!(monsterDetails.constitution.ToString() == null)) monsterCard.AddField("Constitution", monsterDetails.constitution, true);
            if (!(monsterDetails.intelligence.ToString() == null)) monsterCard.AddField("Intelligence", monsterDetails.intelligence, true);
            if (!(monsterDetails.wisdom.ToString() == null)) monsterCard.AddField("Wisdom", monsterDetails.wisdom, true);
            if (!(monsterDetails.charisma.ToString() == null)) monsterCard.AddField("Charisma", monsterDetails.charisma, true);

            monsterCard.AddField("—————————————————————————————————", "\u200b", false);

            if (monsterDetails.strength_save != null) monsterCard.AddField("Str Save", monsterDetails.strength_save, true);
            if (monsterDetails.dexterity_save != null) monsterCard.AddField("Dex Save", monsterDetails.dexterity_save, true);
            if (monsterDetails.constitution_save != null) monsterCard.AddField("Con Save", monsterDetails.constitution_save, true);
            if (monsterDetails.intelligence_save != null) monsterCard.AddField("Int Save", monsterDetails.intelligence_save, true);
            if (monsterDetails.wisdom_save != null) monsterCard.AddField("Wis Save", monsterDetails.wisdom_save, true);
            if (monsterDetails.charisma_save != null) monsterCard.AddField("Char Save", monsterDetails.charisma_save, true);


            if (!(monsterDetails.damage_vulnerabilities == "")) monsterCard.AddField("Vulnerabilities", monsterDetails.damage_vulnerabilities, true);
            if (!(monsterDetails.damage_resistances == "")) monsterCard.AddField("Resistances", monsterDetails.damage_resistances, true);
            if (!(monsterDetails.damage_immunities == "")) monsterCard.AddField("Immunities", monsterDetails.damage_immunities, true);
            if (!(monsterDetails.senses == "")) monsterCard.AddField("Senses", monsterDetails.senses, true);
            if (!(monsterDetails.languages == "")) monsterCard.AddField("Languages", monsterDetails.languages, true);
            if (!(monsterDetails.challenge_rating.ToString() == null)) monsterCard.AddField("Challenge Rating", monsterDetails.challenge_rating, true);

            monsterCard.AddField("—————————————————————————————————", "\u200b", false);

            //if (!(monsterDetails.history.ToString() == null)) monsterCard.AddField("History", monsterDetails.history, true);
            //if (!(monsterDetails.perception.ToString() == null)) monsterCard.AddField("Perception", monsterDetails.perception, true);
            //if (!(monsterDetails.medicine == null)) monsterCard.AddField("Medicine", monsterDetails.medicine, true);
            //if (!(monsterDetails.religion == null)) monsterCard.AddField("Religion", monsterDetails.religion, true);
            //if (!(monsterDetails.stealth == null)) monsterCard.AddField("Stealth", monsterDetails.stealth, true);
            //if (!(monsterDetails.persuasion == null)) monsterCard.AddField("Persuasion", monsterDetails.persuasion, true);
            //if (!(monsterDetails.insight == null)) monsterCard.AddField("Insight", monsterDetails.insight, true);
            //if (!(monsterDetails.deception == null)) monsterCard.AddField("Deception", monsterDetails.deception, true);
            //if (!(monsterDetails.arcana == null)) monsterCard.AddField("Arcana", monsterDetails.arcana, true);
            //if (!(monsterDetails.athletics == null)) monsterCard.AddField("Athletics", monsterDetails.athletics, true);
            //if (!(monsterDetails.acrobatics == null)) monsterCard.AddField("Acrobatics", monsterDetails.acrobatics, true);
            //if (!(monsterDetails.survival == null)) monsterCard.AddField("Survival", monsterDetails.survival, true);
            //if (!(monsterDetails.investigation == null)) monsterCard.AddField("Investigation", monsterDetails.investigation, true);
            //if (!(monsterDetails.nature == null)) monsterCard.AddField("Nature", monsterDetails.nature, true);
            //if (!(monsterDetails.intimidation == null)) monsterCard.AddField("Intimidation", monsterDetails.intimidation, true);
            //if (!(monsterDetails.performance == null)) monsterCard.AddField("Performance", monsterDetails.performance, true);

            return monsterCard;
        }

        private EmbedBuilder MonsterActionBuilder(MonsterDetails monsterDetails)
        {
            var monsterCardActions = new EmbedBuilder();
            monsterCardActions.WithTitle(monsterDetails.name);

            monsterCardActions.WithColor(Color.Red);
            //SPECIAL ABILITIES
            if (monsterDetails.special_abilities != null)
            {
                monsterCardActions.AddField("SPECIAL ABILITIES", "\u200b", false);
                foreach (var x in monsterDetails.special_abilities) monsterCardActions.AddField(x.name, x.desc);
                monsterCardActions.AddField("—————————————————————————————————", "\u200b", false);
            }
            //ACTIONS
            if (monsterDetails.actions != null)
            {
                monsterCardActions.AddField("ACTIONS", "\u200b", false);
                foreach (var x in monsterDetails.actions) monsterCardActions.AddField(x.name, $"{x.desc}");
                monsterCardActions.AddField("—————————————————————————————————", "\u200b", false);

            }
            //LEGENDARY ACTIONS
            if (monsterDetails.legendary_actions != null)
            {
                monsterCardActions.AddField("LEGENDARY ACTIONS", "\u200b", false);
                foreach (var x in monsterDetails.legendary_actions) monsterCardActions.AddField(x.name, x.desc);
                monsterCardActions.AddField("—————————————————————————————————", "\u200b", false);

            }
            //REACTIONS
            if (monsterDetails.reactions != null)
            {
                monsterCardActions.AddField("REACTIONS", "\u200b", false);
                foreach (var x in monsterDetails.reactions) monsterCardActions.AddField(x.name, x.desc);
                monsterCardActions.AddField("—————————————————————————————————", "\u200b", false);

            }

            return monsterCardActions;
        }




        private MonsterDetails ProduceInfo(int index)
        {
            using (var webClient = new WebClient())
            {
                string rawJSON = webClient.DownloadString($"http://dnd5eapi.co/api/monsters/{index}");
                MonsterDetails MonsterInfo = JsonConvert.DeserializeObject<MonsterDetails>(rawJSON);
                return MonsterInfo;
            }
        }

        private Dictionary<string, string> CreateArrayOfAllMonsters()
        {
            using (var webClient = new WebClient())
            {
                //get string representation of JSON
                string rawJSON = webClient.DownloadString("http://dnd5eapi.co/api/monsters");
                //convert string to RootObjects
                MonsterList MonsterList = JsonConvert.DeserializeObject<MonsterList>(rawJSON);
                var MonsterDictionary = new Dictionary<string, string>();

                for (int i = 0; i < MonsterList.count; i++)
                {
                    MonsterDictionary.Add(MonsterList.results[i].name.ToLower(), MonsterList.results[i].url);
                }
                return MonsterDictionary;
            }
        }
    }
}
