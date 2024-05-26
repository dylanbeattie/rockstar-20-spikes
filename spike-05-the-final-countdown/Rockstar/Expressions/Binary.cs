using System.Text;

namespace Rockstar.Expressions;

public enum Operator {
	Plus,
	Minus,
	Times,
	Divide,
	And,
	Or,
	Equals,
	Not,
	Nor,
	LessThanEqual,
	GreaterThanEqual,
	LessThan,
	GreaterThan
}

public class Binary(Operator op, Expression lhs, Expression rhs) : Expression {
	public Operator Op => op;
	public Expression Lhs => lhs;
	public Expression Rhs => rhs;

	public object Resolve(Func<Expression, object> eval) {
		var lhsValue = eval(lhs);
		var rhsValue = eval(rhs);
		return op switch {
			Operator.Plus => Plus(lhsValue, rhsValue),
			Operator.Minus => Minus(lhsValue, rhsValue),
			Operator.Times => Times(lhsValue, rhsValue),
			Operator.Divide => Divide(lhsValue, rhsValue),
			_ => throw new InvalidOperationException()
		};
	}

	private object Divide(object l, object r) => (l, r) switch {
		(decimal a, decimal b) => a / b,
		_ => throw new InvalidOperationException()
	};

	private object Times(object l, object r) => (l, r) switch {
		(decimal a, decimal b) => a * b,
		_ => throw new InvalidOperationException()
	};

	private object Plus(object l, object r) => (l, r) switch {
		(decimal a, decimal b) => a + b,
		(string a, string b) => a + b,
		_ => throw new InvalidOperationException()
	};

	private object Minus(object l, object r) => (l, r) switch {
		(decimal a, decimal b) => a - b,
		_ => throw new InvalidOperationException()
	};

	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"{op}:".ToLowerInvariant());
		lhs.Print(sb, depth + 1);
		rhs.Print(sb, depth + 1);
	}
}