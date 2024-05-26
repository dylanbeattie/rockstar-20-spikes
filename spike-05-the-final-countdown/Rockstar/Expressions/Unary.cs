using System.Text;

namespace Rockstar.Expressions;

public class Unary(Operator op, Expression expr) : Expression {
	public Operator Op => op;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine("unary: {op}");
		expr.Print(sb, depth+1);
	}
}