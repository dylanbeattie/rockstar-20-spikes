using System.Collections;

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
			case '(':
				SkipComment();
				return null;
			case ' ':
			case '\r':
			case '\t':
				return null;
			case '\n':
				line++;
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

	//private char PeekNext => current + 1 >= source.Length ? '\0' : source[current + 1];
	public static readonly Dictionary<TokenType, string[]> Keywords = new() {
		{ TokenType.Output, ["shout", "say", "whisper", "scream"] },
		{ TokenType.Null, ["null", "nothing", "nowhere", "nobody", "gone"] },
		{ TokenType.True, [ "true", "right", "yes", "ok" ] },
		{ TokenType.False, [ "false", "wrong", "no", "lies" ] },
		{ TokenType.Mysterious, [ "mysterious"] },
		{ TokenType.String, [ "empty"] },
		{ TokenType.Plus, ["plus", "with"] },
		{ TokenType.Minus, ["minus", "without"] },
		{ TokenType.Star, ["times", "of" ] },
		{ TokenType.Slash, ["over",] }
	};

	private Token ScanIdentifier() {
		while (Peek.IsAlphaNumeric()) Next();
		var lexeme = source[start..current];
		var tokenTypes = Keywords.Match(lexeme);
		throw new NotImplementedException();
		//if (tokenTypes != TokenType.String) return Token(tokenType);
		//return lexeme.ToLowerInvariant() switch {
		//	"empty" => Token(TokenType.String, String.Empty),
		//	_ => Token(tokenType)
		//};
	}
}


public static class KeywordExtensions {
	private static readonly KeyValuePair<TokenType, string[]> identifier = new(TokenType.Identifier, []);

	public static TokenType[] Match(this Dictionary<TokenType, string[]> keywords, string text)
		=> keywords.Where(pair
			=> pair.Value.Any(keyword
				=> keyword.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)))
			.Select(pair => pair.Key).ToArray();
}
