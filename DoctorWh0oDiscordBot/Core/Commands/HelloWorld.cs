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
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "hello world"), Summary("Hello world command")]
        public async Task greeting(string anything)
        {
            await Context.Channel.SendMessageAsync("Sup, asshole");
        }
    }

    public class PicTest : ModuleBase<SocketCommandContext>
    {
        [Command("pictest"), Summary("testing ability to post images")]
        public async Task pic()
        {
            await Context.Channel.SendFileAsync(@"C:\Users\awessels\source\repos\DoctorWh0oDiscordBot\DoctorWh0oDiscordBot\Data\atEveryone.jpg");
        }
    }
}
