namespace Rockstar.Expressions;

public class True(int line, int column, string? lexeme = default)
	: Expression(line, column, lexeme), IAmTruthy {
	public bool Truthy => true;
}