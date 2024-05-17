using System.Globalization;
using System.Text;

namespace Rockstar;

public class AstPrinter : Expr.IVisitor<string> {

	public string Print(Expr expr) => expr.Accept(this);

	public string Visit(Expr.Binary expr) => Parenthesize(expr.Op.Lexeme, expr.Lhs, expr.Rhs);

	public string Visit(Expr.Grouping expr) => Parenthesize("group", expr.Expr);

	public string Visit(Expr.Literal expr) => expr.Value?.ToString() ?? "nil";

	public string Visit(Expr.Unary expr) => Parenthesize(expr.Op.Lexeme, expr.Expr);

	private string Parenthesize(string name, params Expr[] nodes) {
		var sb = new StringBuilder();
		sb.Append("(").Append(name);
		foreach (var node in nodes) sb.Append(" ").Append(node.Accept(this));
		sb.Append(")");
		return sb.ToString();
	}
}
