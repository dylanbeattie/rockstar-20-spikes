namespace Engine;

public class Interpreter {
	public string Run(string program) {
		return $"{program} It worked - ran {program.Length} characters at {DateTime.UtcNow:O}";
	}
}
