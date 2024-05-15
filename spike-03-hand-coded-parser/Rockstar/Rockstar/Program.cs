namespace Rockstar;

public class Program {
	public static void Main(string[] args) {
		GeneratedClass.GeneratedMethod();
		var b = new BumbleClass("horse");
		b.Method();

		switch (args.Length) {
			case > 1:
				Console.WriteLine("Usage: rockstar <program.rock>");
				Environment.Exit(64);
				break;
			case 1:
				RunFile(args[1]);
				break;
			default:
				RunPrompt();
				break;
		}
	}

	private static void RunFile(string path) {
		var contents = File.ReadAllText(path);
		Run(contents);
		if (hadError) Environment.Exit(65);
	}

	private static void RunPrompt() {
		while (true) {
			Console.Write("> ");
			var line = Console.ReadLine();
			if (line == null) break;
			Run(line);
			hadError = false;
		}
	}
	private static void Run(string source) {
		var scanner = new Scanner(source);
		var tokens = scanner.ScanTokens();
		foreach (var token in tokens) Console.WriteLine(token);
	}

	public static void Error(int line, string message) => Report(line, "", message);

	private static bool hadError = false;

	private static void Report(int line, string where, string message) {
		Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
		hadError = true;
	}
}
