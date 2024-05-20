namespace Rockstar;

public static class KeywordExtensions {
	private static readonly char[] whitespaces = [' ', '\t', '\n', '\r'];
	public static string NormalizeSpacing(this string input) {
		var words = input.Split(whitespaces, StringSplitOptions.RemoveEmptyEntries);
		return String.Join(' ', words);
	}

	public static TokenType[] PartialMatches(
		this Dictionary<TokenType, string[]> keywords, string text)
			=> keywords.Where(pair
					=> pair.Value.Any(keyword
						=> keyword.StartsWith(text + ' ', StringComparison.InvariantCultureIgnoreCase)))
				.Select(pair => pair.Key).ToArray();

	public static bool TryPerfectMatch(
		this Dictionary<TokenType, string[]> keywords, string text, out TokenType tokenType) {
		var normalizedText = text.NormalizeSpacing();
		if (!keywords.Any(k
				=> k.Value.Contains(normalizedText, StringComparer.InvariantCultureIgnoreCase))) {
			tokenType = TokenType.Undefined;
			return false;
		}
		tokenType = keywords.Single(k
			=> k.Value.Contains(normalizedText, StringComparer.InvariantCultureIgnoreCase)).Key;
		return true;
	}
}
