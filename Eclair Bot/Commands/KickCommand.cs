using Discord.Commands;
using Discord.WebSocket;
using System;
using Discord;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclair_Bot.Commands
{
    public class KickCommand : ModuleBase<SocketCommandContext>
    {
        [Command("kick1")]

        public async Task Kick(SocketGuildUser mention, string reason = null)
        {
            if (mention != null)
            {

                await mention.KickAsync();

                await ReplyAsync(reason == null ? $"I've successfully kicked the user {mention.Username} for no reason." : $"I've successfuly kicked {mention.Username} for the reason  {reason}");

                await mention.KickAsync();


            }
        }
    }
}
