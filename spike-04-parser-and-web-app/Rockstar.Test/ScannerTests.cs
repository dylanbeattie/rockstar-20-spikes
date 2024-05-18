using Shouldly;

namespace Rockstar.Test;

public class AmazingScannerTests {
	private static void Error(int line, string error) => throw new($"{line}: {error}");

	[Theory]
	[InlineData("()")]
	[InlineData("(this is a comment)")]
	public void ScannerIgnoresComments(string source) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Count.ShouldBe(1);
		tokens[0].Type.ShouldBe(TokenType.Eof);
	}
	[Theory]
	[InlineData(" \n \n \n")]
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
	[InlineData("\n\"bar\nbaz\"\n", 3)]
	public void ScannerMatchesMultilineStrings(string source, int lineNumber) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Select(t => t.Type).ShouldBe([TokenType.String, TokenType.Eof]);
		tokens.Select(t => t.Line).ShouldBe([3,4]);
	}

	[Theory]
	[InlineData("0")]
	[InlineData("1")]
	[InlineData("123.45")]
	public void ScannerMatchesNumberLiterals(string source) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Select(t => t.Type).ShouldBe([TokenType.Number, TokenType.Eof]);
	}

	[Theory]
	[InlineData("-0")]
	[InlineData("-1")]
	[InlineData("-123.45")]
	public void ScannerMatchesUnaryNegativeNumberLiterals(string source) {
		var tokens = new Scanner(source, Error).Tokens.ToList();
		tokens.Select(t => t.Type).ShouldBe([TokenType.Minus, TokenType.Number, TokenType.Eof]);
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
