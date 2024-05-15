public class Scanner(string source) {
	private int start = 0;
	private int current = 0;
	private int line = 1;
	private bool IsAtEnd => current >= source.Length;
	public IEnumerable<Token> ScanTokens() {
		while (!IsAtEnd) {
			var token = ScanToken();
			if (token != default) yield return token;
		}
		yield return Token(TokenType.EOF);
	}

	private Token? ScanToken() {
		start = current;
		var c = Next();
		switch (c) {
			case '"': return ScanString();
			case '/':
				if (!Match('/')) return Token(TokenType.SLASH);
				while (Peek() != '\n' && !IsAtEnd) Next();
				return null;
			case '!': return Token(Match('=') ? TokenType.NOT_EQUAL : TokenType.UNARY_NOT);
			case '=': return Token(Match('=') ? TokenType.DOUBLE_EQUALS : TokenType.EQUALS);
			case '<': return Token(Match('=') ? TokenType.LESS_THAN_EQUALS : TokenType.LESS_THAN);
			case '>': return Token(Match('=') ? TokenType.GREATER_THAN_EQUALS : TokenType.GREATER_THAN);
			case '+': return Token(TokenType.PLUS);
			case '*': return Token(TokenType.TIMES);
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
				Rockstar.Error(line, "Unexpected character.");
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

	private char Peek() => IsAtEnd ? '\0' : source[current];
	private char PeekNext() => current + 1 >= source.Length ? '\0' : source[current + 1];

	private Token ScanNumber() {
		while (Peek().IsDigit()) Next();
		if (Peek() == '.' && PeekNext().IsDigit()) {
			Next();
			while (Peek().IsDigit()) Next();
		}
		return Token(TokenType.NUMBER, Decimal.Parse(source[start..current]));
	}

	private Token ScanString() {
		while (Peek() != '"' && !IsAtEnd) {
			if (Peek() == '\n') line++;
			Next();
		}
		if (IsAtEnd) {
			Rockstar.Error(line, "Unterminated string.");
		} else {
			Next();
		}
		var literal = source[start..current].Trim('"');
		return Token(TokenType.STRING, literal);
	}

	private static Dictionary<string, TokenType> keywords = new() {
		{ "shout", TokenType.PRINT }
	};

	private Token ScanIdentifier() {
		while (Peek().IsAlphaNumeric()) Next();
		var text = source[start..current];
		if (keywords.TryGetValue(text, out var tokenType)) return Token(tokenType);
		return Token(TokenType.IDENTIFIER);
	}
}
