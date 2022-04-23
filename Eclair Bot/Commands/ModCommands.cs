using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Commands
{
    [Name("Moderation Commands")]
    public class ModerationModule : ModuleBase<SocketCommandContext>
    {

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Kick a user")]
        public async Task Kick([Summary("user to kick")] SocketGuildUser user = null)
        {
            var eb = new EmbedBuilder();
            eb.WithDescription("Недостаточно аргументов.");
            eb.WithColor(new Color(255, 0, 0));

            var eb1 = new EmbedBuilder();
            eb1.WithDescription($"<@!{user.Id}> был кикнут.");
            eb1.WithColor(new Color(255, 0, 0));

            await Context.Channel.TriggerTypingAsync();

            if (user == null)
            {
                await ReplyAsync("", false, eb.Build());
                return;
            }

            await user.KickAsync();
            await Context.Channel.SendMessageAsync("", false, eb1.Build());
        }


        [Command("mute")]
        [Summary("mute a user")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task Mute([Summary("The user to mute")] SocketGuildUser user,
            [Summary("Number of minutes to mute for")] int minutes = 10,
            [Summary("The reason for muting")][Remainder] string reason = null)
        {
            await Context.Channel.TriggerTypingAsync();
            var eb1 = new EmbedBuilder();
            eb1.WithDescription($"Администратора нельзя замутить!");
            eb1.WithColor(new Color(255, 0, 0));

            var eb2 = new EmbedBuilder();
            eb2.WithDescription("Этот пользователь уже замучен!");
            eb2.WithColor(new Color(255, 0, 0));

            var eb3 = new EmbedBuilder();
            eb3.WithDescription($"Затычка выдана: <@!{user.Id}> " + $"\nВремя мута: {minutes} минут\nПричина: {reason ?? "None"}");
            eb3.WithColor(new Color(255, 0, 0));

            if (user.Hierarchy > Context.Guild.CurrentUser.Hierarchy)
            {
                await Context.Channel.SendMessageAsync("", false, eb1.Build());
                return;
            }

            // Check for muted role, attempt to create it if it doesn't exist
            var role = (Context.Guild as IGuild).Roles.FirstOrDefault(x => x.Name == "Muted");
            if (role == null)
            {
                role = await Context.Guild.CreateRoleAsync("Muted", new GuildPermissions(sendMessages: false), null, false);
            }

            if (role.Position > Context.Guild.CurrentUser.Hierarchy)
            {
                await Context.Channel.SendMessageAsync("", false, eb1.Build());
                return;
            }

            if (user.Roles.Contains(role))
            {
                await Context.Channel.SendMessageAsync("", false, eb2.Build());
                return;
            }

            await role.ModifyAsync(x => x.Position = Context.Guild.CurrentUser.Hierarchy);
            foreach (var channel in Context.Guild.Channels)
            {
                if (!channel.GetPermissionOverwrite(role).HasValue || channel.GetPermissionOverwrite(role).Value.SendMessages == PermValue.Allow)
                {
                    await channel.AddPermissionOverwriteAsync(role, new OverwritePermissions(sendMessages: PermValue.Deny));
                }
            }

    //        MuteHandler.AddMute(new Mute { Guild = Context.Guild, User = user, End = DateTime.Now + TimeSpan.FromMinutes(minutes), Role = role });
            await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync("", false, eb3.Build());
        }

        [Command("unmute")]
        [Summary("unmute a user")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        private async Task Unmute([Summary("The user to unmute")] SocketGuildUser user)
        {
            var eb1 = new EmbedBuilder();
            eb1.WithDescription("У пользователя нет мута.");
            eb1.WithColor(new Color(255, 0, 0));

            var eb2 = new EmbedBuilder();
            eb2.WithDescription("Администратора нельзя размутить!");
            eb2.WithColor(new Color(255, 0, 0));

            var eb3 = new EmbedBuilder();
            eb3.WithDescription($"Пользователь <@!{user.Id}> , успешно размучен.");
            eb3.WithColor(new Color(255, 0, 0));

            var role = (Context.Guild as IGuild).Roles.FirstOrDefault(x => x.Name == "Muted");
            if (role == null)
            {
                await Context.Channel.SendMessageAsync("", false, eb1.Build());
                return;
            }

            if (role.Position > Context.Guild.CurrentUser.Hierarchy)
            {
                await Context.Channel.SendMessageAsync("", false, eb2.Build());
                return;
            }

            if (!user.Roles.Contains(role))
            {
                await Context.Channel.SendMessageAsync("", false, eb1.Build());
                return;
            }

            await user.RemoveRoleAsync(role);
            await Context.Channel.SendMessageAsync("", false, eb3.Build());
        }
    }
}
