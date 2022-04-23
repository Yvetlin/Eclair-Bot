using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclair_Bot.Commands
{
    public class FloodCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Flood")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task FloodAsync()
        {
            ulong channelid = Context.Channel.Id;

            for (int i = 0; i < 50; i++)
            {
                await ReplyAsync($"skip");
                Thread.Sleep(1000);
            }
        }
    }
}
