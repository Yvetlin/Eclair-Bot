using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclair_Bot.Commands
{
    public class TesterCommand : ModuleBase<SocketCommandContext>
    {
        [Command("givetest")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task testAsync(string user)
        {
            var user1 = Context.User as SocketGuildUser;
            var userId = user.Replace("<@", "");
            userId = userId.Replace(">", "");

            Action<ulong> addRole = (x) => Context.Client.GetGuild(888917788058587186).GetUser(Convert.ToUInt64(userId)).AddRoleAsync(x); //в цифрах айди сервера
            addRole(940744965397549126); //айди роли

            var eb = new EmbedBuilder();
            eb.WithDescription($"<@!{userId}> Виу!");
            eb.WithColor(new Color(191, 255, 0));

            await Context.Channel.SendMessageAsync("", false, eb.Build());


            //var user1 = Context.User as SocketGuildUser;
            //var userId = user.Replace("<@!", "");
            //userId = userId.Replace(">", "");

            //Console.WriteLine(user);
            //Console.WriteLine(user1);


        }
    }
}