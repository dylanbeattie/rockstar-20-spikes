using Shouldly;

namespace Rockstar.Test;

public class KeywordTests {

	private static void Error(int line, string error) => throw new($"{line}: {error}");

	public static IEnumerable<object[]> Keywords() {
		foreach (var pair in Scanner.Keywords) {
			foreach (var keyword in pair.Value) yield return [keyword, pair.Key];
		}
	}

	[Theory]
	[MemberData(nameof(Keywords))]
	public void ScannerMatchesPrimary(string source, TokenType token) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Count.ShouldBe(2);
		tokens[0].Type.ShouldBe(token);
		tokens[1].Type.ShouldBe(TokenType.Eof);
	}
	[Fact]
	public void KeywordMatcherIsGreedy() {
		var keywords = new Dictionary<TokenType, string[]> {
			{ TokenType.EqualSign, ["is"] },
			{ TokenType.GreaterThan, ["is greater than"] }
		};
		keywords.Match("is").ShouldBe([TokenType.EqualSign, TokenType.GreaterThan]);
		keywords.Match("is greater ").ShouldBe([ TokenType.GreaterThan ]);
		keywords.Match("is y").ShouldBe([]);
	}
}
