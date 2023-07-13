// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Trip.Client.SignalR;

string url = "https://localhost:43210";
//string baseUrl = "https://localhost:44376";
var connection = new HubConnectionBuilder()
               .WithUrl($"{url}/videohub", (opts) =>
               {
                   opts.HttpMessageHandlerFactory = (message) =>
                   {
                       if (message is HttpClientHandler clientHandler)
                           // bypass SSL certificate
                           clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
                       return message;
                   };
               }).ConfigureLogging(logging =>
               {
                   logging.SetMinimumLevel(LogLevel.Information);
                   logging.AddConsole();
               }).WithAutomaticReconnect(new RetryPolicy())
               .Build();

connection.ServerTimeout = TimeSpan.FromMinutes(2);

await connection.StartAsync();

connection.On<string, string, string>("VideoChange", async (a, b, c) =>
{
    await Console.Out.WriteLineAsync($"Video {a} is here {b} {c}");
});

connection.On<string, string, string>("StatusChanged", async (a, b, c) =>
{
    await Console.Out.WriteLineAsync($"Video {a} is here {b} {c}");
});

await connection.InvokeAsync("StartWatching");

string exitCode = "";

do
{
    exitCode = Console.ReadLine();
} while (exitCode != "shutdown");

