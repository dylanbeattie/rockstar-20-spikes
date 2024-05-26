using System.Text;
using Rockstar.Expressions;

namespace Rockstar.Statements;

public class Assign(Variable name, Expression expr) : Statement {
	public string Name => name.Name;
	public Expression Expr => expr;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine("assign: {name}");
		expr.Print(sb, depth + 1);
	}
}