
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

namespace DoctorWh0oDiscordBot
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            //string JSON = "";
            //string SettingsLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Replace(@"bin\Debug\netcoreapp2.0", @"Data\Settings.json");
            //using (var Stream = new FileStream(SettingsLocation, FileMode.Open, FileAccess.Read))
            //using (var ReadSettings = new StreamReader(Stream))
            //{
            //    JSON = ReadSettings.ReadToEnd();
            //}

            //Setting Settings = JsonConvert.DeserializeObject<Setting>(JSON);
            //ESettings.Banned = Settings.banned;
            //ESettings.Log = Settings.log;
            //ESettings.Owner = Settings.owner;
            //ESettings.Token = Settings.token;
            //ESettings.Version = Settings.version;

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            //help help HELP heLp
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

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


            //ESettings.Token = File.ReadLine(@"C:\Users\awessels\source\repos\DoctorWh0oDiscordBot\settings.txt");


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

            int ArgPos = 0;
            if (!(Message.HasStringPrefix("a!", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos, services: null);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
        }
    }
}