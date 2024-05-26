using System.Text;

namespace Rockstar.Expressions;

public abstract class Expression(int line, int column, string? lexeme = default) {
	public abstract void Print(StringBuilder sb, int depth);

	protected string Location => lexeme == default
		? $"(line {line}, column {column})"
		: $"(line {line}, column {column - lexeme.Length}, lexeme {lexeme})";
}
