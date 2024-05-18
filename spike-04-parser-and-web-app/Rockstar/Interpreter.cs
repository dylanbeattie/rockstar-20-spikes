namespace Rockstar;

public class Interpreter(IAmARockstarEnvironment env) : IVisitor<object?> {
	public int Run(IEnumerable<Statement> program) {
		foreach (var statement in program) Execute(statement);
		return 0;
	}

	private void Execute(Statement statement)
		=> statement.Accept(this);

	public object? Visit(Expr.String expr) => expr.Value;
	public object? Visit(Expr.Number expr) => expr.Value;

	public object? Visit(Statement.Output stmt) {
		var value = Visit(stmt.Expr);
		env.WriteLine(value?.ToString() ?? "null");
		return null;
	}

	public object? Visit(Statement.Expression expr)
		=> Visit(expr.Expr);

	public object? Visit(Expr.Unary expr) {
		var value = Visit(expr.Expr);
		if (expr.Type == TokenType.Minus) return -(decimal)value!;
		throw new NotImplementedException();
	}

	private object? Visit(Expr exprExpr)
		=> exprExpr.Accept(this);
}