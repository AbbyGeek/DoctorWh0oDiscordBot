using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace DoctorWh0oDiscordBot.Core.Commands
{
    public class atEveryone : ModuleBase<SocketCommandContext>
    {
        public async Task everyone()
        {
        await Context.Channel.SendFileAsync(@"C:\Users\awessels\source\repos\DoctorWh0oDiscordBot\DoctorWh0oDiscordBot\Data\atEveryone.jpg");

        }

    }
}
