using System.Globalization;
using System.Text;

namespace Rockstar;

public class AstPrinter : ExprNode.IVisitor<string> {

	public string Print(ExprNode expr) => expr.Accept(this);

	public string Visit(ExprNode.BinaryNode expr) => Parenthesize(expr.Op.Lexeme, expr.Lhs, expr.Rhs);

	public string Visit(ExprNode.GroupingNode expr) => Parenthesize("group", expr.Expr);

	public string Visit(ExprNode.StringNode expr) => expr.Value;

	public string Visit(ExprNode.NumberNode expr) => expr.Value.ToString(CultureInfo.InvariantCulture);

	public string Visit(ExprNode.UnaryNode expr) => Parenthesize(expr.Op.Lexeme, expr.Expr);

	private string Parenthesize(string name, params ExprNode[] nodes) {
		var sb = new StringBuilder();
		sb.Append("(").Append(name);
		foreach (var node in nodes) sb.Append(" ").Append(node.Accept(this));
		sb.Append(")");
		return sb.ToString();
	}
}
