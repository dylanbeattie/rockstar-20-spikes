namespace Engine;

public class Engine {
	public string Run(string program) {
		return $"{program} It worked - ran {program.Length} characters at {DateTime.UtcNow:O}";
	}
}
