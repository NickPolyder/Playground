using Microsoft.AspNetCore.SignalR.Client;
using Playground.SignalR.Shared;

namespace Playground.SignalR.Client;

public class ChatClient : IChatClient, IAsyncDisposable
{
    private readonly HubConnection _connection;
    private readonly IDisposable[] _subscriptions;

    public ChatClient(HubConnection connection, string username)
    {
        var chatServer = new ChatServer(username);
        _connection = connection;
        _subscriptions = new[]
        {
            _connection.On<string, string>(nameof(chatServer.ReceiveMessage), chatServer.ReceiveMessage)
        };
    }

    public Task StartAsync(CancellationToken cancellationToken = default) => _connection.StartAsync(cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken = default) => _connection.StopAsync(cancellationToken);

    /// <inheritdoc />
    public Task SendMessage(string user, string message)
    {
        return _connection.InvokeAsync(nameof(SendMessage), user, message);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        foreach (var subscription in _subscriptions)
        {
            subscription.Dispose();
        }

        await _connection.DisposeAsync();
    }

    private class ChatServer : IChatServer
    {
        private readonly string _username;

        public ChatServer(string username)
        {
            _username = username;
        }
        /// <inheritdoc />
        public Task ReceiveMessage(string user, string message)
        {
            if (_username != user)
            {
                Console.WriteLine($"{user}: {message}");
            }
            return Task.CompletedTask;
        }

    }
}