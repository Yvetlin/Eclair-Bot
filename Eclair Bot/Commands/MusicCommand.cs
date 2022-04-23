using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclair_Bot.Commands
{
    public class MusicCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Music")]
        public async Task FloodAsync()
        {
            ulong channelid = Context.Channel.Id;

            for (int i = 0; i < 3; i++)
            {
                await ReplyAsync($".p Бандана Платина");
                Thread.Sleep(10000);
                await ReplyAsync($".p lil krystall - Каждый день");
                Thread.Sleep(10000);
                await ReplyAsync($".p lil krystall - Кукла");
                Thread.Sleep(10000);
                await ReplyAsync($".p rizza Плачь");
                Thread.Sleep(10000);
                await ReplyAsync($".p Плохой HOROSHIYAGNI");
                Thread.Sleep(10000);
                await ReplyAsync($".p Monkey long Kizaru");
                Thread.Sleep(10000);

            }
        }
    }
}
