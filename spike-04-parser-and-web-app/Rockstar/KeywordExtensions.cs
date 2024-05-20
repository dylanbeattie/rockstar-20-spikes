namespace Rockstar;

public static class KeywordExtensions {
	public static Dictionary<TokenType, MatchType> Match(
		this Dictionary<TokenType, string[]> keywords, string text)
		=> keywords.ToDictionary(pair => pair.Key, pair => pair.Value.Match(text))
			.Where(pair => pair.Value != MatchType.NoMatch)
			.ToDictionary();

	public static MatchType Match(this string[] keywords, string text) {
		if (keywords.Any(keyword
			    => keyword.Equals(text, StringComparison.InvariantCultureIgnoreCase)))
			return MatchType.Complete;
		return keywords.Any(keyword
			=> keyword.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)) ? MatchType.Partial : MatchType.NoMatch;
	}
}
