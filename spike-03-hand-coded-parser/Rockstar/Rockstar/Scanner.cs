

public class Scanner(string source) {
	private int start = 0;
	private int current = 0;
	private int line = 1;

	private bool IsAtEnd => current >= source.Length;

	public IEnumerable<Token> ScanTokens() {
		while (!IsAtEnd) {
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

	private bool Match(char expected) {
		if (IsAtEnd || source[current] != expected) return false;
		current++;
		return true;
	}

	private char Peek() {
		return IsAtEnd ? '\0' : source[current];
	}

	private Token ScanToken() {
		while (true) {
			var c = advance();
			switch (c) {
				case '!': return (Token(Match('=') ? TokenType.NOT_EQUAL : TokenType.UNARY_NOT);
				case '=': return Token(Match('=') ? TokenType.DOUBLE_EQUALS : TokenType.EQUALS);
				case '<': return Token(Match('=') ? TokenType.LESS_THAN_EQUALS : TokenType.LESS_THAN);
				case '>': return Token(Match('=') ? TokenType.GREATER_THAN_EQUALS : TokenType.GREATER_THAN);
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
}
