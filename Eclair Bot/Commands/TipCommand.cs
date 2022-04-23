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
    public class TipCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Tip")]
        public async Task TipAsync(string user)
        {

            var usernum = Context.User as SocketUser;
            var userId = user.Replace("<@", "");
            userId = userId.Replace(">", "");

            var userIdNum = long.Parse(user.Replace("<@", "").Replace(">", ""));
            if (userIdNum.Equals(457307609490522132))
                await ReplyAsync($"<@!{usernum.Id}>, хорошо сыграно!");
            else
                await ReplyAsync($"<@!{userIdNum}>, хорошо сыграно!");

        }
    }
}

