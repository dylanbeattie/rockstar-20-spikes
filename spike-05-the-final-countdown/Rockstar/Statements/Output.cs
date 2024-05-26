using System.Text;
using Rockstar.Expressions;

namespace Rockstar.Statements;

public class Output(Expression expr, int line, int column) : Statement(line,column) {
	public Expression Expr => expr;
	public override string ToString() => $"output: {expr}";

	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine("output:");
		expr.Print(sb, depth + 1);
	}
}