namespace Rockstar;

public interface IVisitStatements<out T> {
	T Visit(Statement.Output stmt);
	T Visit(Statement.Expression expr);
	T Visit(Statement.Assignment stmt);
}

public interface IVisitStatements {
	void Visit(Statement.Output stmt);
	void Visit(Statement.Expression expr);
	void Visit(Statement.Assignment stmt);
}

public interface IVisitExpressions<out T> {
	T Visit(Expr.String expr);
	T Visit(Expr.Number expr);
	T Visit(Expr.Unary expr);
	T Visit(Expr.Null expr);
	T Visit(Expr.False expr);
	T Visit(Expr.True expr);
	T Visit(Expr.Mysterious expr);
	T Visit(Expr.Binary expr);
	T Visit(Expr.Variable expr);
}
