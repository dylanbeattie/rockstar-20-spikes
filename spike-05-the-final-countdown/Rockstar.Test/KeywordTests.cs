using Shouldly;

namespace Rockstar.Test;

public class KeywordTests {

	private static void Error(int line, string error) => throw new($"{line}: {error}");

	public static IEnumerable<object[]> Keywords() {
		foreach (var pair in Scanner.Keywords) {
			foreach (var keyword in pair.Value) {
				if (keyword.EndsWith(" ")) continue;
				yield return [keyword, pair.Key];
			}
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
}
