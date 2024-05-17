namespace Rockstar;

public class ConsoleEnvironment : IAmARockstarEnvironment {
	public string? ReadInput() => Console.ReadLine();
	public void WriteOutput(string output) => Console.WriteLine(output);
}