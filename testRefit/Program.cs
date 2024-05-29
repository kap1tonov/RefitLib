using Refit;
using System.Net.Http.Headers;
using testRefit;

public class DiscordApiClient
{
    private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://discord.com/api/") };

    public static async Task Main(string[] args)
    {
        string token = "наш токен";

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var discordApi = RestService.For<IDiscordApi>(Client);
        var userApi = RestService.For<IUserApi>(Client);

        var servers = await userApi.GetServersAsync(token);
        Console.WriteLine("Список серверов пользователя:");
        int index = 1;
        foreach (var server in servers)
        {
            Console.WriteLine($"{index++}. {server.Name}");
        }
        Console.WriteLine("Введите номер сервера для просмотра каналов:");

        if (int.TryParse(Console.ReadLine(), out int serverIndex) && serverIndex > 0 && serverIndex <= servers.Count())
        {
            var selectedServer = servers.ElementAt(serverIndex - 1);
            var serverId = selectedServer.Id;

            var channels = await userApi.GetChannelsAsync(serverId, token);
            Console.WriteLine("Каналы сервера: " + selectedServer.Name);
            index = 1;
            foreach (var channel in channels)
            {
                Console.WriteLine($"{index++}. {channel.Name}");
            }
            Console.WriteLine("Введите номер канала для просмотра сообщений");
            if(int.TryParse(Console.ReadLine(), out int channelIndex) && channelIndex > 0 && channelIndex <= channels.Count())
            {
                var selectedChannel = channels.ElementAt(channelIndex - 1);
                var channelId = selectedChannel.Id;

                Console.WriteLine("Введите кол-во сообщений");
                if (int.TryParse(Console.ReadLine(), out int messagesCount) && messagesCount > 0)
                {
                    var messages = await userApi.GetMessagesAsync(channelId, token, messagesCount);
                    index = 1;
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"{index++}. " + message.Content);
                    }
                    Console.WriteLine("Введите номер сообщения для добавления реакции:");
                    if (int.TryParse(Console.ReadLine(), out int messageIndex) && messageIndex > 0)
                    {
                        var selectedMessage = messages.ElementAt(messageIndex - 1);
                        var messageId = selectedMessage.Id;
                        var emoji = "🍆";
                        await userApi.CreateReactionAsync(channelId, messageId, emoji, token);
                        Console.WriteLine("Реакция на сообщение была успешно добавлена");
                    }
                }         
            }
        }
    }
}

/*
Получение серверов пользователя
Получение каналов определённого сервера по Id сервера
Получение последних X сообщений с определённого канала по Id канала
Реакция на определённое сообщение по Id сообщения
Реакция на определённые сообщения по Id сообщений
 */