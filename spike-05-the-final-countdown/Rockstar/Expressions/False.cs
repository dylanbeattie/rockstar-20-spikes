using System.Text;

namespace Rockstar.Expressions;

public class False(int line, int column, string? lexeme = default)
	: Expression(line, column, lexeme), IAmTruthy {
	public bool Truthy => false;
}