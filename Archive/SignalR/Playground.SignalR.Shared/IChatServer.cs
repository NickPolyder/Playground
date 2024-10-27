namespace Playground.SignalR.Shared;

public interface IChatServer
{
    Task ReceiveMessage(string user, string message);
}

public interface IChatClient
{
    Task SendMessage(string user, string message);
}