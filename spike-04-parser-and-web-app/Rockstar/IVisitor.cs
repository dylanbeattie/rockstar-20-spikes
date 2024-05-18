namespace Rockstar;

public interface IVisitor<out T> {
	T Visit(Expr.String expr);
	T Visit(Statement.Output stmt);
	T Visit(Statement.Expression expr);
	T Visit(Expr.Number expr);
	T Visit(Expr.Unary expr);
}
