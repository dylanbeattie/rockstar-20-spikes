namespace Rockstar;

public class Interpreter(IAmARockstarEnvironment env) : IVisitor<object?> {
	public int Run(IEnumerable<Statement> program) {
		foreach (var statement in program) Execute(statement);
		return 0;
	}

	private void Execute(Statement statement)
		=> statement.Accept(this);

	public object? Visit(Expr.String expr) => expr.Value;

	public object? Visit(Statement.Output stmt) {
		var value = Evaluate(stmt.Expr);
		env.WriteLine(value?.ToString() ?? "null");
		return null;
	}

	public object? Evaluate(Expr expr) => expr.Accept(this);

	public object? Visit(Statement.Expression expr)
		=> Visit(expr.Expr);

	private object? Visit(Expr exprExpr)
		=> exprExpr.Accept(this);
}