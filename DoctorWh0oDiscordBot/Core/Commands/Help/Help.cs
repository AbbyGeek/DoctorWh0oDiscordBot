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
    public class Help : ModuleBase<SocketCommandContext>
    {
        const string UserAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

        [Command("help"), Alias("Help")]
        public async Task helpMe(string message)
        {
           
            EmbedBuilder HelpCard = HelpCardMaker();
            await Context.Channel.SendMessageAsync("", false, HelpCard.Build());
        }


        private EmbedBuilder HelpCardMaker()
        {
            var helpCard = new EmbedBuilder();
            helpCard.WithTitle("Hello there, My name is 'Who'. I'm here to help you with your DnD questions about spells and monsters");



            helpCard.WithColor(Color.Blue);
            helpCard.AddField("!MonsterSearch 'search term', 'search value'", $"this command is for searching for all monsters with a certain stat. Try using 'size', armor_class', 'hit_points', strength, dexterity, constitution, wisdom, intelligence, charisma, or 'challenge_rating' as a search term. Be sure to indicate the value you are searching for.", true);
            helpCard.AddField("—————————————————————————————————", "\u200b", false);
            helpCard.AddField("!Monster 'monster name'", "This command will allow you to search for details about a specific monster. If the monster has spaces in their name, be sure to use quotations around their name", true);
            helpCard.AddField("—————————————————————————————————", "\u200b", false);
            helpCard.AddField("!spell 'spell name'", "This command allows you to examine a spell card for any spell in the game. If your spell has more than one word in it, be sure to surround it with quotation marks.", true);
            helpCard.AddField("—————————————————————————————————", "\u200b", false);
            helpCard.AddField("!SpellList 'insert anything here' ", "This command will list each and every spell that you can search for. Be warned, it takes a minute to list them all. Be sure to put SOMETHING after the 'spelllist' or the command won't work.", true);


            return helpCard;
        }
    }
}