namespace Rockstar;

public static class Program {
	public static void Main(string[] args) {
		var env = new ConsoleEnvironment();
		switch (args.Length) {
			case > 1:
				Console.WriteLine("Usage: rockstar <program.rock>");
				Environment.Exit(64);
				break;
			case 1:
				RunFile(args[1], env);
				break;
			default:
				RunPrompt(env);
				break;
		}
	}

	static void RunFile(string path, ConsoleEnvironment env) {
		var contents = File.ReadAllText(path);
		Run(contents, env);
		if (hadError) Environment.Exit(65);
	}

	static void RunPrompt(ConsoleEnvironment env) {
		while (true) {
			env.WriteOutput("> ");
			var line = env.ReadInput();
			if (line == null) break;
			Run(line, env);
			hadError = false;
		}
	}

	static void Run(string source, IAmARockstarEnvironment env) {
		var scanner = new Scanner(source, Error);
		var parser = new Parser();
		var abstractSyntaxTree = parser.Parse(scanner.Tokens);
		var interpreter = new Interpreter(env);
		interpreter.Run(abstractSyntaxTree);
	}

	public static void Error(int line, string message) => Report(line, "", message);

	private static bool hadError = false;

	private static void Report(int line, string where, string message) {
		Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
		hadError = true;
	}
}