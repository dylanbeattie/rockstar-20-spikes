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
			case ' ':
			case '\t':
			case '\r':
			case '\n': return null;
			case '(': return SkipComment(')');
			case '{': return SkipComment('}');
			case '[': return SkipComment(']');
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

	private Token? SkipComment(char end) {
		while (Peek != end && !IsAtEnd) Next();
		if (IsAtEnd) {
			error(line, "Unterminated comment.");
		} else {
			Next();
		}
		return null;
	}

	private Token ScanString() {
		while (Peek != '"' && !IsAtEnd) Next();
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

	private char Next() {
		var c = source[current++];
		if (c == '\n') line++;
		return c;
	}

	private char Peek => IsAtEnd ? '\0' : source[current];

	private Token Token(TokenType type) => Token(type, null);

	private Token Token(TokenType type, object? literal)
		=> new(type, source[start..current], literal, line);

	private static string[] IsThan(params string[] words)
		=> words.Select(w => $"is {w} than").ToArray();

	private static string[] IsAsAs(params string[] words)
		=> words.Select(w => $"is as {w} as").ToArray();

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
		{ TokenType.AreEqual, ["is"] },
		{ TokenType.NotEqual, ["is not", "are not", "isn't", "ain't"] },
		{ TokenType.GreaterThan, IsThan("greater", "higher", "bigger", "stronger" ) },
		{ TokenType.LessThan, IsThan("less", "lower", "smaller", "weaker" ) },
		{ TokenType.LessThanEqual, IsAsAs("low", "little", "small", "weak" )},
		{ TokenType.GreaterThanEqual, IsAsAs("high", "great", "big", "strong" ) },

		// Note these keywords have a trailing space, so the tokenizer
		// will consume the following identifier as well.
		{ TokenType.CommonVariablePrefix, [ "a ", "an ", "the ", "my ", "your ", "our ",]}
	};

	private string LookAhead(int offset) {
		var i = offset;
		while (i < source.Length && source[i].IsWhitespace()) i++;
		if (i < source.Length && source[i].IsAlpha()) {
			while (i < source.Length && source[i].IsIdentifier()) i++;
		}
		return source[offset..i];
	}

	private Token ScanIdentifier()
		=> ScanIdentifier("", current - 1, default);

	private Token ScanIdentifier(string keyword, int lookaheadFrom, TokenType match = default) {
		var next = LookAhead(lookaheadFrom);
		var potential = keyword + next;
		var partialMatch = Keywords.IsPartialMatch(potential) && next.Length > 0;
		var perfectMatch = Keywords.IsPerfectMatch(potential, out var found);
		if (perfectMatch) {
			match = found;
			if (!partialMatch) return Consume(potential, match);
		}
		return partialMatch switch {
			false when match != default => Consume(keyword, match),
			false => Consume(potential, TokenType.Identifier),
			_ => ScanIdentifier(potential, lookaheadFrom + next.Length, match)
		};
	}

	private Token Consume(string lexeme, TokenType token) {
		current += lexeme.Length - 1;
		return Token(token);
	}
}