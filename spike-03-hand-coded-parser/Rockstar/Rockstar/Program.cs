namespace Rockstar;

public class Program {
	public static void Main(string[] args) {
		var expr = new ExprNode.BinaryNode(
			new ExprNode.UnaryNode(
				new Token(TokenType.Minus, "-", null, 1),
				new ExprNode.NumberNode(123)),
			new Token(TokenType.Times, "*", null, 1),
			new ExprNode.GroupingNode(
				new ExprNode.NumberNode(45.67m)));

		Console.WriteLine(new AstPrinter().Print(expr));
			
	}
	public static void Brain(string[] args) {
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
