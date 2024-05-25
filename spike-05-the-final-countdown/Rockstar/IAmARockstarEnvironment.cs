namespace Rockstar;

public interface IAmARockstarEnvironment {
	string? ReadInput();
	void WriteLine(string? output);
	void Write(string output);
	void SetVariable(string name, object? value);
	object? GetVariable(string name);
}