using System.Diagnostics.CodeAnalysis;

namespace Rockstar;

public class Token(TokenType type, string lexeme = "", object? literal = null, int line = 0) {
	public TokenType Type => type;
	public object? Literal => literal;
	public string Lexeme => lexeme;
	public int Line => line;
	public override string ToString() => $"{type} {lexeme} {literal} (line: {0})";
}