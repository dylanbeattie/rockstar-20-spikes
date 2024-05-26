using Rockstar.Expressions;

namespace Rockstar;

public class ConsoleEnvironment : IAmARockstarEnvironment {
	public string? ReadInput() => Console.ReadLine();
	public void WriteLine(string? output) => Console.WriteLine(output);
	public void Write(string output) => Console.Write(output);
	private readonly Dictionary<string, object?> variables = new();
	public void SetVariable(string name, object? value) => variables[name] = value;
	public object GetVariable(string name)
		=> variables[name] ?? Mysterious.Instance;
}