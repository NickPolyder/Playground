// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Playground.SignalR.Client;

Console.WriteLine("Hello, Chat!");

Console.Write("Pick a Username: ");
var username = Console.ReadLine() ?? throw new ArgumentNullException("Username");

Console.WriteLine();


var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7295/chat")
    .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
    .Build();


await using var chatClient = new ChatClient(connection, username);

await chatClient.StartAsync();

bool inLoop = true;

Console.CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    inLoop = false;
};

Console.WriteLine("Write a message then press enter!");

while (inLoop)
{
    var message = Console.ReadLine();
   
    if (!string.IsNullOrWhiteSpace(message))
    {
        await chatClient.SendMessage(username, message);
    }
    else
    {
        Console.WriteLine("Please write something before hitting sent.");
    }
}

