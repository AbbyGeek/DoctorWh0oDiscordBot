using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using System.Reflection;
using System.IO;

namespace DoctorWh0oDiscordBot.Core.Commands
{
    class Greeting : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello world command")]
        public async Task HelloWorld()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }
    }
}
