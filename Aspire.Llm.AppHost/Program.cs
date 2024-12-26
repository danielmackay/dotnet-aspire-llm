var builder = DistributedApplication.CreateBuilder(args);

// Add Ollama server
var ollama =
    builder.AddOllama("ollama")
        .WithDataVolume()
        .WithOpenWebUI();

// Add chat model
var chat = ollama.AddModel("chat", "llama3.2");

builder.AddProject<Projects.Aspire_Llm_Console>("console")
    .WithReference(chat)
    .WaitFor(chat);

builder.Build().Run();
