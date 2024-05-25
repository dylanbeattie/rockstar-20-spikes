using System.Globalization;

namespace Rockstar.Expressions;

public class Number(decimal value) : Expression {
	public decimal Value => value;
	public override string ToString() => value.ToString(CultureInfo.InvariantCulture);
}