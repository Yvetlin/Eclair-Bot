using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EclairBot.Commands
{
    public class PushCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Push")]
        public async Task PushAsync()
        {
            ulong channelid = Context.Channel.Id;

            if (!channelid.Equals(937382518418727002))
                return;

            for (int i = 0; i < 20; i++)
            {
                await ReplyAsync($"<@!457307609490522132> вставай!");
                Thread.Sleep(1000);
            }
        }
    }
}
