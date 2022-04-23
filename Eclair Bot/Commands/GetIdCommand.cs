using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EclairBot.Commands
{
    public class GetIdCommand : ModuleBase<SocketCommandContext>
    {
        [Command("getid")]
        public async Task GetIdAsync(string user)
        {
            ulong channelid = Context.Channel.Id;

            if (!channelid.Equals(889227038093893663))
                return;

            var iam = Context.User;
            var userId = user.Replace("<@", "");
            userId = userId.Replace(">", "");
            var eb = new EmbedBuilder();

            eb.WithDescription($"<@!{iam.Id}>, у <@!{userId}> ID : {userId}");
            eb.WithColor(new Color(255, 255, 255));

            await Context.Channel.SendMessageAsync("", false, eb.Build());
            

        }
        [Command("getid")]
        public async Task GetIdAsync()
        {
            ulong channelid = Context.Channel.Id;

            if (!channelid.Equals(889227038093893663))
                return;

            var user = Context.User;
            var eb = new EmbedBuilder();

            eb.WithDescription($"<@!{user.Id}>, Ваш ID : {user.Id}");
            eb.WithColor(new Color(255, 255, 255));

            await Context.Channel.SendMessageAsync("", false, eb.Build());

        }
    }
}

