using System.Text;

namespace Rockstar.Expressions;

public class Strïng(string value, int line = 0, int column = 0, string? lexeme = default)
	: Expression(line, column, lexeme), IAmTruthy {
	public string Value => value;
	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine($"string: \"{value}\"");

	public bool Truthy => !String.IsNullOrEmpty(value);
}