namespace Rockstar.Expressions;

public class Null(int line, int column, string? lexeme = default)
	: Expression(line, column, lexeme), IAmTruthy {
	public bool Truthy => false;
}