using System.Text;

namespace Rockstar.Statements;

public abstract class Statement(int line, int column, string? lexeme = default) {
	public abstract void Print(StringBuilder sb, int depth = 0);
	protected string Location => lexeme == default
		? $"(line {line}, column {column})"
		: $"(line {line}, column {column - lexeme.Length}, lexeme {lexeme})";


}

