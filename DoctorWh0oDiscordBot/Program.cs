
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using DoctorWh0oDiscordBot.Resources.Datatypes;
using DoctorWh0oDiscordBot.Resources.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorWh0oDiscordBot
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private IServiceProvider Services;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            //help help HELP heLp
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Services = new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton(Commands)
                .BuildServiceProvider();


            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;


            string line;
            StreamReader file = new StreamReader(@"C:\Users\awessels\source\repos\DoctorWh0oDiscordBot\settings.txt");
            while((line= file.ReadLine()) != null)
            {
                ESettings.Token = line;
            }

                await Client.LoginAsync(TokenType.Bot, ESettings.Token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{ DateTime.Now} at {Message.Source}] {Message.Message}");
            try
            {
                SocketGuild Guild = Client.Guilds.Where(x => x.Id == ESettings.Log[0]).FirstOrDefault();
                SocketTextChannel Channel = Guild.Channels.Where(x => x.Id == ESettings.Log[1]).FirstOrDefault() as SocketTextChannel;
                await Channel.SendMessageAsync($"{DateTime.Now} at {Message.Source}] {Message.Message}");
            }
            catch { }
        }

        private async Task Client_Ready()
        {
            //await Client.SetActivityAsync("Owl Games");
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);
            

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int argPos = 0;
            if(Message.HasStringPrefix("!", ref argPos) || Message.HasMentionPrefix(Client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(Client, Message);
                var result = await Commands.ExecuteAsync(Context, argPos, Services);
                if (!result.IsSuccess)
                {
                    Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {result.ErrorReason}");
                }
            }
        }
    }
}