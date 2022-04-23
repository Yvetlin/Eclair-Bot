using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace EclairBot.Commands
{
    public class GiveRoleVamCommand : ModuleBase<SocketCommandContext>
    {
        [Command("GiveRole Vam")]
        //     [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task VamTask(string user)
        {
            ulong channelid = Context.Channel.Id;

            if (!channelid.Equals(889227038093893663))
                return;

            var user1 = Context.User as SocketGuildUser;
            var userId = user.Replace("<@", "");
            userId = userId.Replace(">", "");

            var eb = new EmbedBuilder();
            eb.WithDescription($"Пользователю <@!{userId}>, выдана роль - Вампус.");
            eb.WithColor(new Color(191, 255, 0));

            var eb1 = new EmbedBuilder();
            eb1.WithDescription($"<@!{user1.Id}>, у Вас нет прав на выполнение данной команды.");
            eb1.WithColor(new Color(191, 255, 0));

            if (user1.GuildPermissions.Administrator)
            {
                Action<ulong> addRole = (x) => Context.Client.GetGuild(888917788058587186).GetUser(Convert.ToUInt64(userId)).AddRoleAsync(x); //в цифрах айди сервера
                addRole(888921863101747211); //айди роли
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, eb1.Build());
            }

            //     foreach(var role in user.Roles)
            //        Console.WriteLine(role.Name);
        }
    }
}