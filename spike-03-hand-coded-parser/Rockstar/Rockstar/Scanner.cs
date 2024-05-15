namespace Rockstar;

public class Scanner(string source) {
	private int start;
	private int current;
	private int line = 1;
	private bool IsAtEnd => current >= source.Length;
	public IEnumerable<Token> ScanTokens() {
		while (!IsAtEnd) {
			var token = ScanToken();
			if (token != default) yield return token;
		}
		yield return Token(TokenType.Eof);
	}

	private Token? ScanToken() {
		start = current;
		var c = Next();
		switch (c) {
			case '"': return ScanString();
			case '/':
				if (!Match('/')) return Token(TokenType.Slash);
				while (Peek != '\n' && !IsAtEnd) Next();
				return null;
			case '!': return Token(Match('=') ? TokenType.NotEqual : TokenType.UnaryNot);
			case '=': return Token(Match('=') ? TokenType.DoubleEquals : TokenType.Equals);
			case '<': return Token(Match('=') ? TokenType.LessThanEquals : TokenType.LessThan);
			case '>': return Token(Match('=') ? TokenType.GreaterThanEquals : TokenType.GreaterThan);
			case '+': return Token(TokenType.Plus);
			case '*': return Token(TokenType.Times);
			case ' ':
			case '\r':
			case '\t':
				return null;
			case '\n':
				line++;
				return null;
			default:
				if (c.IsDigit()) return ScanNumber();
				if (c.IsAlpha()) return ScanIdentifier();
				Program.Error(line, "Unexpected character.");
				return null;
		}
	}

	private char Next() => source[current++];

	private Token Token(TokenType type) => Token(type, null);

	private Token Token(TokenType type, object? literal)
		=> new(type, source[start..current], literal, line);

	private bool Match(char expected) {
		if (IsAtEnd || source[current] != expected) return false;
		current++;
		return true;
	}

	private char Peek => IsAtEnd ? '\0' : source[current];
	private char PeekNext => current + 1 >= source.Length ? '\0' : source[current + 1];

	private Token ScanNumber() {
		while (Peek.IsDigit()) Next();
		if (Peek != '.' || !PeekNext.IsDigit()) return Token(TokenType.Number, Decimal.Parse(source[start..current]));
		Next();
		while (Peek.IsDigit()) Next();
		return Token(TokenType.Number, Decimal.Parse(source[start..current]));
	}

	private Token ScanString() {
		while (Peek != '"' && !IsAtEnd) {
			if (Peek == '\n') line++;
			Next();
		}
		if (IsAtEnd) {
			Program.Error(line, "Unterminated string.");
		} else {
			Next();
		}
		var literal = source[start..current].Trim('"');
		return Token(TokenType.String, literal);
	}

	private static readonly Dictionary<TokenType, string[]> keywords = new() {
		{ TokenType.Print, ["shout", "say", "whisper", "scream"] }
	};

	private TokenType MatchToken(string identifier)
		=> keywords.FirstOrDefault(pair => pair.Value.Contains(identifier, StringComparer.InvariantCultureIgnoreCase)).Key;

	private Token ScanIdentifier() {
		while (Peek.IsAlphaNumeric()) Next();
		var text = source[start..current];
		var tokenType = MatchToken(text);
		return Token(tokenType);
	}
}