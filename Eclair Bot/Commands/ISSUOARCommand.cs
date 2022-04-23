using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace EclairBot.Commands
{
    public class ISSUOARCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ISSUOAR")]
     //   [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task ISSUOARTask(string user)
        {
            

            var user1 = Context.User as SocketGuildUser;
            var userId = user.Replace("<@", "");
            userId = userId.Replace(">", "");

            var eb = new EmbedBuilder();
            eb.WithDescription($"Вжух!");
            eb.WithColor(new Color(191, 255, 0));

            var eb1 = new EmbedBuilder();
            eb1.WithDescription($"<@!{user1.Id}>, у Вас нет прав на выполнение данной команды.");
            eb1.WithColor(new Color(191, 255, 0));

            if (user1.GuildPermissions.Administrator)
            {
                var guild = Context.Client.GetGuild(888917788058587186);
                if (guild == null)
                {
                    debug("guild null");
                }
                var targetUser = guild.GetUser(Convert.ToUInt64(userId));
                debug("1");

                if (targetUser == null)
                {
                    debug("User null");
                }
                targetUser.AddRoleAsync(888918119400222731); //айди роли
                debug("3");

                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, eb1.Build());
            }

              //   foreach(var role in user.Roles)
            //        Console.WriteLine(role.Name);
        }
        public async void debug(string message)
        {
            Console.WriteLine(message);
        }
    }
}