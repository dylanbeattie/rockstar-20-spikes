using System.Globalization;
using System.Text;

namespace Rockstar.Expressions;

public class Number(decimal value) : Expression {
	public decimal Value => value;
	public override string ToString() => value.ToString(CultureInfo.InvariantCulture);

	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine($"number: {value}");
}