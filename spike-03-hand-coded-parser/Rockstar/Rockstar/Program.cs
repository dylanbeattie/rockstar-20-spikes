
public class Rockstar {
	public static void Main(string[] args) {
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

	public static void Error(int line, string message) {
		Report(line, "", message);
	}

	private static bool hadError = false;

	private static void Report(int line, string where, string message) {
		Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
		hadError = true;
	}
}

public enum TokenType {
	PRINT, NUMBER, PLUS, TIMES, MINUS, DIVIDE, EOF
}

public class Token(TokenType type, string lexeme,object literal, int line) {
	public override string ToString() {
		return $"{type} {lexeme} {literal}";
	}
}

public class Scanner(string source) {
	private int start = 0;
	private int current = 0;
	private int line = 1;

	private bool IsAtEnd() => current >= source.Length;
	
	IEnumerable<Token> ScanTokens() {
		while (!IsAtEnd()) {
			start = current;
			yield return ScanToken();
		}
		yield return Token(TokenType.EOF);
	}

	private char advance() => source[current++];

	private Token Token(TokenType type) => Token(type, null);

	private Token Token(TokenType type, object literal) {
		var text = source.Substring(start, current);
		return new(type, text, literal, line);
	}

	private Token ScanToken() {
		var c = advance();
		switch (c) {
			case ' ':
			case '\r':
			case '\t':
				// Ignore whitespace.
				break;
			case '\n':
				line++;
				break;
			case '+': return Token(TokenType.PLUS);
			case '*': return Token(TokenType.TIMES);
			default:
				Rockstar.Error(line, "Unexpected character.");
				break;
		}
	}

}