using System.Globalization;
using System.Text;

namespace Rockstar.Expressions;

public class Number(decimal value, int line, int column, string lexeme) : Expression(line,column,lexeme) {
	public decimal Value => value;
	public override string ToString() => value.ToString(CultureInfo.InvariantCulture);

	public override void Print(StringBuilder sb, int depth)
			=> sb.Indent(depth).AppendLine($"number: {value:G29} {Location}");
}