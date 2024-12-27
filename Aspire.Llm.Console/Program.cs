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

var app = builder.Build();

var chatClient = app.Services.GetRequiredKeyedService<IChatClient>("chat");

// Add messages for memory
var messages = new List<ChatMessage>();

string input;

while (true)
{
    Console.Write("Query: ");
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
        break;
    messages.Add(new ChatMessage { Text = input });
    // var response = await chatClient.CompleteAsync(messages);

    // Stream response
    var stream = chatClient.CompleteStreamingAsync(messages);
    await foreach (var chunk in stream)
        Console.Write(chunk.Text);

    var completion = await stream.ToChatCompletionAsync();
    messages.AddRange(completion.Choices);
    Console.WriteLine();
    // Console.WriteLine("Result: " + response.Message);
}

Console.WriteLine("Exiting...");
