namespace Rockstar;

public static class KeywordExtensions {
	private static readonly char[] whitespaces = [' ', '\t', '\n', '\r'];
	public static string NormalizeSpacing(this string input) {
		var words = input.Split(whitespaces, StringSplitOptions.RemoveEmptyEntries);
		return String.Join(' ', words);
	}

	public static bool IsPartialMatch(this Dictionary<TokenType, string[]> keywords, string text)
		=> keywords.Any(pair
			=> pair.Value.Any(keyword
				=> keyword.StartsWith(text + ' ', StringComparison.InvariantCultureIgnoreCase)));


	public static bool IsPerfectMatch(
		this Dictionary<TokenType, string[]> keywords, string text, out TokenType tokenType) {
		var normalizedText = text.NormalizeSpacing();
		if (!keywords.Any(k => k.ContainsKeyword(normalizedText))) {
			tokenType = TokenType.Undefined;
			return false;
		}
		tokenType = keywords.Single(k => k.ContainsKeyword(normalizedText)).Key;
		return true;
	}

	public static bool ContainsKeyword(this KeyValuePair<TokenType, string[]> pair, string text)
		=> pair.Value.Contains(text, StringComparer.InvariantCultureIgnoreCase);
}
