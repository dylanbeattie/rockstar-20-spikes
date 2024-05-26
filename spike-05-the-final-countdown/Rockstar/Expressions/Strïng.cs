using System.Text;

namespace Rockstar.Expressions;

public class Strïng(string value) : Expression {
	public string Value => value;
	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine("string: \"{value}\"");
}