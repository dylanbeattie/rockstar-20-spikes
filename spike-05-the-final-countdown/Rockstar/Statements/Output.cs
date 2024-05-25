using Rockstar.Expressions;

namespace Rockstar.Statements;

public class Output(Expression expr) : Statement {
	public Expression Expr => expr;
	public override string ToString() => $"output: {expr}";
}