using Newtonsoft.Json.Linq;
using Shouldly;

namespace Rockstar.Test;

public class ScannerTests {
	private static void Error(int line, string error) => throw new Exception($"{line}: {error}");

	[Theory]
	[InlineData("""
	            
	            
	            """)]
	[InlineData(" ")]
	[InlineData("\n\n\t\r\n")]
	public void ScannerIgnoresNewlinesAndWhitespace(string source) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Count.ShouldBe(1);
		tokens[0].Type.ShouldBe(TokenType.Eof);
	}

	[Theory]
	[InlineData("\"foo\"")]
	[InlineData("\"\"")]
	public void ScannerMatchesStringLiterals(string source) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Select(t => t.Type).ShouldBe([ TokenType.String, TokenType.Eof ]);
	}

	[Theory]
	[InlineData("""
	            
	            "bar
	            baz"
	            
	            """, 3)]
	public void ScannerMatchesMultilineStrings(string source, int lineNumber) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Select(t => t.Type).ShouldBe([TokenType.String, TokenType.Eof]);
		tokens.Select(t => t.Line).ShouldBe([3,4]);
	}

	[Theory]
	[InlineData("shout")]
	[InlineData("scream")]
	[InlineData("whisper")]
	[InlineData("say")]
	public void ScannerMatchesOutputKeywords(string source) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Select(t => t.Type).ShouldBe([TokenType.Output, TokenType.Eof]);
	}
}

public class TestCaseTest {
	[Fact]
	public void ExpectationExtractorExtractsExpectation() {
		var expect = Fixtures.ExtractExpects("""
		                                     shout "hello" (expect: hello)
		                                     shout "world" (expect: world)
		                                     """);
		expect.ShouldBe("""
		                hello
		                world
		                """);
	}
}
