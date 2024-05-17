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
			case ' ':
			case '\r':
			case '\t':
				return null;
			case '\n':
				line++;
				return null;
			case '"': return ScanString();
			default:
				if (c.IsAlpha()) return ScanIdentifier();
				error(line, "Unexpected character");
				return null;
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

	private static readonly Dictionary<TokenType, string[]> keywords = new() {
		{ TokenType.Output, ["shout", "say", "whisper", "scream"] }
	};

	private Token ScanIdentifier() {
		while (Peek.IsAlphaNumeric()) Next();
		var lexeme = source[start..current];
		var tokenType = keywords.Match(lexeme);
		return Token(tokenType);
	}
}


public static class KeywordExtensions {
	private static readonly KeyValuePair<TokenType, string[]> identifier = new(TokenType.Identifier, []);

	public static TokenType Match(this Dictionary<TokenType, string[]> keywords, string text)
		=> keywords.FirstOrDefault(pair
			=> pair.Value.Contains(text, StringComparer.InvariantCultureIgnoreCase),
			identifier
			).Key;
}
