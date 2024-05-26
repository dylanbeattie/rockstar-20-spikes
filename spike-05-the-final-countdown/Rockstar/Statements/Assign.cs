using System.Text;
using Rockstar.Expressions;

namespace Rockstar.Statements;

public class Assign(Variable name, Expression expr, int line, int column) : Statement(line,column) {
	public string Name => name.Name;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"assign:");
		name.Print(sb, depth + 1);
		expr.Print(sb, depth + 1);
	}
}