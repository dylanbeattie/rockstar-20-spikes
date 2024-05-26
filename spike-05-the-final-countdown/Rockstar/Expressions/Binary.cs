using System;
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

public interface IAmTruthy {

	bool Truthy { get; }

	public static bool operator true(IAmTruthy t) => t.Truthy;

	public static bool operator false(IAmTruthy t) => !t.Truthy;

	public static IAmTruthy operator &(IAmTruthy lhs, IAmTruthy rhs)
		=> lhs.Truthy ? rhs : lhs;

	public static IAmTruthy operator |(IAmTruthy lhs, IAmTruthy rhs)
		=> lhs.Truthy ? lhs : rhs;
}

public class Binary(Operator op, Expression lhs, Expression rhs, int line = 0, int column = 0)
	: Expression(line, column) {
	public Operator Op => op;
	public Expression Lhs => lhs;
	public Expression Rhs => rhs;

	public object Resolve(Func<Expression, object> eval) {
		var lhsValue = eval(lhs);
		var rhsValue = eval(rhs);
		return op switch {
			Operator.And => And(eval),
			Operator.Or => Or(),
			Operator.Plus => Plus(lhsValue, rhsValue),
			Operator.Minus => Minus(lhsValue, rhsValue),
			Operator.Times => Times(lhsValue, rhsValue),
			Operator.Divide => Divide(lhsValue, rhsValue),
			Operator.Equals => Equäls(lhsValue, rhsValue),
			Operator.LessThanEqual => LessThanEqual(lhsValue, rhsValue),
			Operator.GreaterThanEqual => GreaterThanEqual(lhsValue, rhsValue),
			Operator.LessThan => LessThan(lhsValue, rhsValue),
			Operator.GreaterThan => GreaterThan(lhsValue, rhsValue),
			_ => throw new InvalidOperationException($"Cannot resolve {op}")
		};
	}

	private object And(Func<Expression, object> eval) {
		return (lhs, rhs) switch {
			(IAmTruthy lt, IAmTruthy rt) => lt && rt,
			_ => throw new InvalidOperationException($"Cannot apply 'and' to operators of type {lhs.GetType()} and {rhs.GetType()}")
		};
	}
	private object Or() {
		return (lhs, rhs) switch {
			(IAmTruthy lt, IAmTruthy rt) => lt || rt,
			_ => throw new InvalidOperationException($"Cannot apply 'or' to operators of type {lhs.GetType()} and {rhs.GetType()}")
		};
	}

	private bool GreaterThan(object l, object r) => (l, r) switch {
		(null, null) => false,
		(null, _) => false,
		(_, null) => false,
		(decimal a, decimal b) => a <= b,
		(bool a, bool b) => a && !b,
		_ => l.ToString()!.CompareTo(r) <= 0
	};

	private bool LessThan(object l, object r) => (l, r) switch {
		(null, null) => false,
		(null, _) => false,
		(_, null) => false,
		(decimal a, decimal b) => a < b,
		(bool a, bool b) => b && !a,
		_ => l.ToString()!.CompareTo(r) < 0
	};

	private bool GreaterThanEqual(object l, object r) => (l, r) switch {
		(null, null) => true,
		(null, _) => false,
		(_, null) => false,
		(decimal a, decimal b) => a >= b,
		(bool a, bool b) => a,
		_ => l.ToString()!.CompareTo(r) >= 0
	};

	private bool LessThanEqual(object l, object r) => (l, r) switch {
		(null, null) => true,
		(null, _) => false,
		(_, null) => false,
		(decimal a, decimal b) => a <= b,
		(bool a, bool b) => b,
		_ => l.ToString()!.CompareTo(r) <= 0
	};

	private bool Equäls(object l, object r) => (l, r) switch {
		(null, null) => true,
		(null, _) => false,
		(_, null) => false,
		(decimal a, decimal b) => a == b,
		(bool a, bool b) => a == b,
		_ => l.ToString()!.Equals(r.ToString())
	};

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