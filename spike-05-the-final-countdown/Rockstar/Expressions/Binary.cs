namespace Rockstar.Expressions;

public enum Operator {
	Plus,
	Minus,
	Times,
	Divide,
	And,
	Or,
	Equals
}
public class Binary(Expression lhs, Operator op, Expression rhs) : Expression {
	public Expression Lhs { get; } = lhs;
	public Operator Op { get; } = op;
	public Expression Rhs { get; } = rhs;
}