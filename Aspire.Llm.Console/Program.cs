using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

// Add in if embedding generator is needed
// builder.AddOllamaSharpEmbeddingGenerator("chat");

builder.AddKeyedOllamaSharpChatClient("chat", s =>
{
    s.Endpoint = new Uri("http://localhost:2200");
    s.SelectedModel = "phi3.5";
});

builder.Services.AddChatClient(sp => sp.GetRequiredKeyedService<IChatClient>("chat"))
    .UseFunctionInvocation()
    .UseOpenTelemetry(configure: t => t.EnableSensitiveData = true)
    .UseLogging();

    // Use the OllamaSharp client
    // .Use());


// builder.Services.AddChatClient( )


var app = builder.Build();

var chatClient = app.Services.GetRequiredKeyedService<IChatClient>("chat");

// Add messages for memory
var messages = new List<ChatMessage>();

string input;
do
{
    Console.Write("Query: ");
    input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
    {
        messages.Add(new ChatMessage { Text = input });
        var response = await chatClient.CompleteAsync(messages);
        messages.AddRange(response.Choices);
        Console.WriteLine("Result: " + response.Message);
    }
} while (!string.IsNullOrEmpty(input));

Console.WriteLine("Exiting...");

// app.Run();

// app.AddCommand((string name, int age) =>
// {
//     Console.WriteLine("Hello, World, name: {0}, age: {1}", name, age);
// });

// app.AddCommands<MyCommand>();
