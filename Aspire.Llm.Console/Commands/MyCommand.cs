namespace Aspire.Llm.Console.Commands;

public class MyCommand
{
    public void Execute(string name, int age)
    {
        System.Console.WriteLine("XXX Hello, World, name: {0}, age: {1}", name, age);
    }
}
