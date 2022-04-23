using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EclairBot.Commands
{
    public class SpecialCommand : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        [Command("Special")]
        public async Task PushAsync(string user)
        {
            ulong channelid = Context.Channel.Id;

            for (int i = 0; i < 2; i++)
            {
                await ReplyAsync($"<@!941631283623231509> ты не придурок, а заичка! :з");
                Thread.Sleep(1000);
            }
        }
    }
}