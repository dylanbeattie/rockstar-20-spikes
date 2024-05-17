namespace Rockstar;

public static class Program {
	private static bool hadError = false;

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
		var scanner = new Scanner(source);
		var parser = new Parser();
		var abstractSyntaxTree = parser.Parse(scanner.Tokens);
		var interpreter = new Interpreter(env);
		interpreter.Run(abstractSyntaxTree);
	}
}

public class Scanner(string source)  {
	public IEnumerable<Token> Tokens {
		get { yield return new(TokenType.Default); }
	}
}

public class Expr;

public class Parser {
	public Expr Parse(IEnumerable<Token> tokens) => new();
}

public interface IAmARockstarEnvironment {
	string? ReadInput();
	void WriteOutput(string output);
}

public class ConsoleEnvironment : IAmARockstarEnvironment {
	public string? ReadInput() => Console.ReadLine();
	public void WriteOutput(string output) => Console.WriteLine(output);
}

public class Interpreter(IAmARockstarEnvironment env) {
	public int Run(Expr expr) => 0;
}

public class Token(TokenType type);

public enum TokenType {
	Default
}