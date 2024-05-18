namespace Rockstar;

public class ConsoleEnvironment : IAmARockstarEnvironment {
	public string? ReadInput() => Console.ReadLine();
	public void WriteLine(string output) => Console.WriteLine(output);
	public void Write(string output) => Console.Write(output);
}