var builder = DistributedApplication.CreateBuilder(args);

// Add Ollama server
var ollama =
        builder
            .AddOllama("ollama", 2200) // 2200
            // .WithGPUSupport()
    // .WithContainerRuntimeArgs("--gpus=all");
            .WithDataVolume()
            .WithOpenWebUI()
    .AddModel("chat", AiModels.Llama)
    ;

// ollama
    // .AddModel("chat", "phi3.5-mini-4k-instruct")
    // .port(5000);
    ;

// Add chat model
// var chat = ollama.AddModel("chat", "llama3.2");

// builder.AddProject<Projects.Aspire_Llm_Console>("console")
//     .WithReference(chat)
//     .WaitFor(chat);

builder.Build().Run();

public static class AiModels
{
    public const string Llama = "llama3.2";
    public const string AllMiniLm = "all-minilm";
    public const string Phi3Mini = "phi3.5";
    public const string Gpt40Mini = "gpt-4o-mini";
}
