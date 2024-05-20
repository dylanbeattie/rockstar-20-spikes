namespace Rockstar;

public static class KeywordExtensions {
	private static readonly char[] whitespaces = [' ', '\t', '\n', '\r'];
	public static string NormalizeSpacing(this string input) {
		var words = input.Split(whitespaces, StringSplitOptions.RemoveEmptyEntries);
		return String.Join(' ', words);
	}

	public static Dictionary<TokenType, MatchType> Match(
		this Dictionary<TokenType, string[]> keywords, string text) {
		var normalizedText = text.NormalizeSpacing();
		return keywords.ToDictionary(pair => pair.Key, pair => pair.Value.Match(normalizedText))
			.Where(pair => pair.Value != MatchType.NoMatch)
			.ToDictionary();
	}

	public static MatchType Match(this string[] keywords, string text) {
		if (keywords.Any(keyword
				=> keyword.Equals(text, StringComparison.InvariantCultureIgnoreCase)))
			return MatchType.Complete;
		return keywords.Any(keyword
			=> keyword.StartsWith(text + ' ', StringComparison.InvariantCultureIgnoreCase)) ? MatchType.Partial : MatchType.NoMatch;
	}
}
