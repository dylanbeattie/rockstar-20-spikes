namespace Rockstar.Expressions;

public class Unary(Operator op, Expression expr) : Expression {
	public Operator Op => op;
	public Expression Expr => expr;
}