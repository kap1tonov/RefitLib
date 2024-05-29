using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static testRefit.DiscordApiLibrary;
using Refit;

namespace testRefit
{

    public class DiscordApiLibrary
    {
        public class Server
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<Channel> Channels { get; set; }
        }

        public class Channel
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
    public interface IUserApi
    {
        [Get("/users/@me/guilds")]
        Task<IEnumerable<Server>> GetServersAsync([Header("Authorization")] string authToken);

        [Get("/guilds/{serverId}/channels")]
        Task<IEnumerable<DiscordApiLibrary.Channel>> GetChannelsAsync(string serverId, [Header("Authorization")] string authToken);

        [Get("/channels/{channelId}/messages")]
        Task<IEnumerable<Message>> GetMessagesAsync(string channelId, [Header("Authorization")] string authToken, [Query] int limit);

        [Put("/channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        Task CreateReactionAsync(string channelId, string messageId, string emoji, [Header("Authorization")] string authToken);

        
    }

    public interface IDiscordApi
    {
        [Get("/users/@me")]
        Task<UserData> GetUserDataAsync([Header("Authorization")] string authToken);
    }

    public class UserData
    {
        public string Username { get; set; }
    }
    public class Channel
    {
        public string Name { get; set; }
    }
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }



    
}

