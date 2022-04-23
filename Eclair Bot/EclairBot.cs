using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace EclairBot
{
    class EclairBot
    {
        static void Main(string[] args)
        {
            try
            {
                new EclairBot().RunBotAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }

        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        public async Task RunBotAsync()
        {
            var config = new DiscordSocketConfig();
            config.GatewayIntents = GatewayIntents.All;
            client = new DiscordSocketClient(config);
            commands = new CommandService();

            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            string token = "OTM3MzI4Mjg3OTcyMjIwOTM4.YfaI_g.hkgiZiHC3XDRw2_9t8CoT_8IoTg";
            
            client.Log += ClientLog;

            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(client, message);
            if (message.Author.IsBot)
                return;

            int argPos = 0;
            if (message.HasStringPrefix("-", ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition))
                    await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private Task ClientLog(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

    }
}
