// See https://aka.ms/new-console-template for more information

using Aspire.Llm.Console.Commands;
using Cocona;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

// app.AddCommand((string name, int age) =>
// {
//     Console.WriteLine("Hello, World, name: {0}, age: {1}", name, age);
// });

app.AddCommands<MyCommand>();

app.Run();
