namespace Rockstar;

public class Scanner(string source, Action<int, string> error) {

	private int start;
	private int current;
	private int line = 1;
	private bool IsAtEnd => current >= source.Length;

	public IEnumerable<Token> Tokens {
		get {
			while (!IsAtEnd) {
				var token = ScanToken();
				if (token == default) {
					start = current;
				} else {
					yield return token;
				}
			}

			yield return Token(TokenType.Eof);
		}
	}

	private Token? ScanToken() {
		start = current;
		var c = Next();
		switch (c) {
			case '\n':
				line++;
				return null;
			case ' ':
			case '\r':
			case '\t':
				return null;
			case '(':
				SkipComment();
				return null;
			case '"': return ScanString();
			case '-': return Token(TokenType.Minus);
			case '+': return Token(TokenType.Plus);
			case '/': return Token(TokenType.Slash);
			case '*': return Token(TokenType.Star);
			default:
				if (c.IsDigit()) return ScanNumber();
				if (c.IsAlpha()) return ScanIdentifier();
				error(line, "Unexpected character");
				return null;
		}
	}

	private void SkipComment() {
		while (Peek != ')' && !IsAtEnd) {
			if (Peek == '\n') line++;
			Next();
		}

		if (IsAtEnd) {
			error(line, "Unterminated comment.");
		} else {
			Next();
		}
	}

	private Token ScanString() {
		while (Peek != '"' && !IsAtEnd) {
			if (Peek == '\n') line++;
			Next();
		}

		if (IsAtEnd) {
			error(line, "Unterminated string.");
		} else {
			Next();
		}

		var literal = source[start..current].Trim('"');
		return Token(TokenType.String, literal);
	}

	private Token ScanNumber() {
		while (Peek.IsDigit()) Next();
		if (Peek == '.') {
			Next();
			while (Peek.IsDigit()) Next();
		}

		var number = Decimal.Parse(source[start..current]);
		return Token(TokenType.Number, number);
	}


	private char Next() => source[current++];

	private Token Token(TokenType type) => Token(type, null);

	private Token Token(TokenType type, object? literal)
		=> new(type, source[start..current], literal, line);

	//private bool Match(char expected) {
	//	if (IsAtEnd || source[current] != expected) return false;
	//	current++;
	//	return true;
	//}

	private char Peek => IsAtEnd ? '\0' : source[current];

	private char PeekNext => current + 1 >= source.Length ? '\0' : source[current + 1];

	public static readonly Dictionary<TokenType, string[]> Keywords = new() {
		{ TokenType.Output, ["shout", "say", "whisper", "scream"] },
		{ TokenType.Null, ["null", "nothing", "nowhere", "nobody", "gone"] },
		{ TokenType.True, ["true", "right", "yes", "ok"] },
		{ TokenType.False, ["false", "wrong", "no", "lies"] },
		{ TokenType.Mysterious, ["mysterious"] },
		{ TokenType.String, ["empty"] },
		{ TokenType.Plus, ["plus", "with"] },
		{ TokenType.Minus, ["minus", "without"] },
		{ TokenType.Star, ["times", "of"] },
		{ TokenType.Slash, ["over"] },
		{ TokenType.EqualSign, ["is"] },
		{ TokenType.GreaterThan, [ "is greater than"] }
	};

	private string Lookahead() {
		var i = current-1;
		while (i < source.Length && source[i].IsWhitespace()) i++;
		if (i < source.Length && source[i].IsAlpha()) {
			while (i < source.Length && source[i].IsAlphaNumeric()) i++;
		}
		return source[(current-1)..i];
	}
	private Token ScanIdentifier() {
		while (true) {
			var lexeme = Lookahead();
			if (String.IsNullOrWhiteSpace(lexeme)) throw new InvalidOperationException("Oops!");
			current += lexeme.Length-1;
			return Token(TokenType.Identifier);
			//while (Peek.IsAlphaNumeric()) Next();
			//// var lexeme = source[start..current];
			//var tokenTypes = Keywords.Match(lexeme);
			//var completeMatches = tokenTypes.Where(t => t.Value == MatchType.Complete).ToList();
			//var partialMatches = tokenTypes.Where(t => t.Value == MatchType.Partial).ToList();
			//if (completeMatches.Count == 1 && partialMatches.Count == 0) {
			//	var tokenType = tokenTypes.First().Key;
			//	if (tokenType != TokenType.String) return Token(tokenType);
			//	return lexeme.ToLowerInvariant() switch {
			//		"empty" => Token(TokenType.String, String.Empty),
			//		_ => Token(tokenType)
			//	};
			//} else if (partialMatches.Count > 0) {

			//}

			//if (tokenTypes.Count == 0) {
			//	// Could be some_variable
			//	// return identifier
			//	// OR keyword followed by identifier
			//}

		}
	}
}

public enum MatchType {
	NoMatch,
	Complete,
	Partial
}
