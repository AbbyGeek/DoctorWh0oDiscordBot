using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DoctorWh0oDiscordBot.Core.Commands;
using DoctorWh0oDiscordBot.Resources.Settings;

namespace DoctorWh0oDiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            _client.MessageReceived += Client_MessageReceived;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);  //needs to work properly or commands will all fail

            _client.Ready += Client_Ready;
            _client.Log += Client_Log;

            string line;
            StreamReader file = new StreamReader(@"C:\Users\awessels\source\repos\DoctorWh0oDiscordBot\settings.txt");
            while ((line = file.ReadLine()) != null)
            {
                ESettings.Token = line;
            }

            await _client.LoginAsync(TokenType.Bot, ESettings.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await _client.SetGameAsync("you sleep", "", ActivityType.Watching);
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(_client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;

            if(Message.Content.Contains("@everyone")) await Context.Channel.SendFileAsync(@"C:\Users\awessels\source\repos\DoctorWh0oDiscordBot\DoctorWh0oDiscordBot\Data\atEveryone.jpg");
            

            if (!(Message.HasStringPrefix("!", ref ArgPos) || Message.HasMentionPrefix(_client.CurrentUser, ref ArgPos))) return;

            var Result = await _commands.ExecuteAsync(Context, ArgPos, null);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");

        }
    }
}