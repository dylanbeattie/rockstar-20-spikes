using System.Text;

namespace Rockstar.Expressions;

public class Looküp(Variable variable, int line, int column, string? lexeme = default)
	: Expression(line, column, lexeme) {
	public Variable Variable => variable;

	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine("lookup: " + variable.Name);
}
