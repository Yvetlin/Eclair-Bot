using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EclairBot.Commands
{
    public class ShuffleCommand : ModuleBase<SocketCommandContext>
    {
        [Command("Shuffle")]
        public async Task ShuffleAsync(string first, string second, string third)
        {
            var channel1 = Context.Guild.GetChannel(ulong.Parse(first)) as ISocketAudioChannel;
            var channel2 = Context.Guild.GetChannel(ulong.Parse(second)) as ISocketAudioChannel;
            var channel3 = Context.Guild.GetChannel(ulong.Parse(third)) as ISocketAudioChannel;

            List<SocketGuildUser> current = new List<SocketGuildUser>();
            IReadOnlyCollection<SocketGuildUser> allUsers = Context.Guild.Users;

            foreach(SocketGuildUser user in allUsers)
            {
                if (user.VoiceChannel == null) continue;
                var id = user.VoiceChannel.Id;
                if (id == channel1.Id)
                    current.Add(user);
            }
            foreach(SocketGuildUser user in current)
            {
                Console.WriteLine(user.Id);
            }
            var rand = new Random();
            var randomList = current.OrderBy(x => rand.Next()).ToList();

            List<SocketGuildUser> list1 = new List<SocketGuildUser>();
            List<SocketGuildUser> list2 = new List<SocketGuildUser>();

            bool desc = true;
            foreach (SocketGuildUser user in current)
            {
                if (desc) 
                    list1.Add(user);
                else
                    list2.Add(user);
                desc = !desc;
            }
            foreach (SocketGuildUser user in list1)
            {
                user.ModifyAsync(x => x.ChannelId = channel2.Id);
                Console.WriteLine(user.Nickname + " " + channel2.Name);
            }
            foreach (SocketGuildUser user in list2)
            {
                user.ModifyAsync(x => x.ChannelId = channel3.Id);
                Console.WriteLine(user.Nickname + " " + channel3.Name);
            }
            
            Console.WriteLine($"{channel1.Name} {channel2.Name} {channel3.Name}");
            
            var eb = new EmbedBuilder();
            eb.WithDescription($"Команды канала {channel1.Name} сформированы");
            eb.WithColor(new Color(65, 105, 225));
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
    }
}