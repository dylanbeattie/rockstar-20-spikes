using System.Text;

namespace Rockstar.Expressions;

public class Unary(Operator op, Expression expr, int line, int column) : Expression(line, column) {
	public Operator Op => op;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine("unary: {op}");
		expr.Print(sb, depth+1);
	}
}