using System;
using System.Threading.Tasks;
using System.Reflection;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;


namespace DoctorWh0oDiscordBot
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private IServiceProvider Services;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Services = new ServiceCollection().AddSingleton(Client).AddSingleton(Commands).BuildServiceProvider();
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            await Client.LoginAsync(TokenType.Bot, "NTUxMTEzMTI2NTc4MjI1MTk0.D1sQRw.RkhWvwkiay3kqBcX83DjjOlROr8");
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage arg)
        {
            throw new NotImplementedException();
        }

        private async Task Client_Ready()
        {
            throw new NotImplementedException();
        }

        private Task Client_MessageReceived(SocketMessage arg)
        {
            throw new NotImplementedException();
        }
    }
}
