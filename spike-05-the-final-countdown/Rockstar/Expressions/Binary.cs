using System;
using System.Text;
using Rockstar.Values;

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



public class Binary(Operator op, Expression lhs, Expression rhs, Source source)
	: Expression(source) {

	private static readonly
		Dictionary<Operator, Func<Value, Value, Value>> ops = new() {
		{ Operator.And, (a, b) => a.And(b) },
		{ Operator.Or, (a,b) => a.Or(b) },
		{ Operator.Plus, (a,b) => a.Plus(b) },
		{ Operator.Minus, (a,b) => a.Minus(b) },
		{ Operator.Times, (a,b) => a.Times(b) },
		{ Operator.Divide, (a,b) => a.Divide(b) },
		{ Operator.Equals, (a,b) => a.Equäls(b) },
		{ Operator.LessThanEqual, (a,b) => a.LessThanEqual(b) },
		{ Operator.GreaterThanEqual, (a,b) => a.MoreThanEqual(b) },
		{ Operator.LessThan, (a,b) => a.LessThan(b) },
		{ Operator.GreaterThan, (a,b) => a.MoreThan(b) },
	};

	public Operator Op => op;
	public Expression Lhs => lhs;
	public Expression Rhs => rhs;

	public Value Resolve(Func<Expression, Value> eval)
		=> ops[op](eval(lhs), eval(rhs));

	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"{op}:".ToLowerInvariant());
		lhs.Print(sb, depth + 1);
		rhs.Print(sb, depth + 1);
	}
}