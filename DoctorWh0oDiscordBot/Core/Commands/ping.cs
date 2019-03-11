using System;
using System.Collections.Generic;
using System.Text;

using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DoctorWh0oDiscordBot.Core.Commands
{
    class ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ping!")
                .WithDescription($"PONG back to you {Context.User.Mention} (This will be a never ending game of Ping/Pong)")
                .WithColor(Color.DarkRed);

            await ReplyAsync("", false, builder.Build());
        }
    }
}