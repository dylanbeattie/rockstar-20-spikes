using System.Text;

namespace Rockstar.Expressions;

public class Variable(string name, int line, int column, string lexeme) : Expression(line, column, lexeme) {
	public string Name => name;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"variable: {name}");
	}
}