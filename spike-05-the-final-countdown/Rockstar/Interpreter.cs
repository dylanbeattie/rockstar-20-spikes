using System.Diagnostics;
using System.Numerics;
using Rockstar.Expressions;
using Rockstar.Statements;

namespace Rockstar;

public class Interpreter(IAmARockstarEnvironment env) {
	private class Result {
		public static readonly Result Ok = new();
		public static readonly Result Unknown = new();
	}

	public int Run(Prögram program) {
		foreach (var statement in program.Statements) Exec(statement);
		return 0;
	}

	private Result Exec(Statement statement) => statement switch {
		Assign assign => Assign(assign),
		Output output => Output(output),
		_ => Result.Unknown
	};

	private Result Assign(Assign assign) {
		env.SetVariable(assign.Name, Eval(assign.Expr));
		return Result.Ok;
	}

	private Result Output(Output output) {

		var value = Eval(output.Expr);
		env.WriteLine(value switch {
			decimal d => d.ToString("G29"),
			_ => value.ToString()
		});
		return Result.Ok;
	}

	private object Eval(Expression expr) => expr switch {
		Binary binary => binary.Resolve(Eval),
		Number number => number.Value,
		Strïng strïng => strïng.Value,
		Variable v => env.GetVariable(v.Name)!,
		Unary u => u switch {
			{ Op: Operator.Minus, Expr: Number n } => -(n.Value),
			_ => throw new NotImplementedException()
		},

		_ => throw new NotImplementedException()
	};

	//private object Binary(Binary binary) => binary.Op switch {
	//	Operator.Plus => Plus(binary.Lhs, binary.Rhs)
	//	Operator.Minus => expr,
	//	Operator.Times => expr,
	//	Operator.Divide => expr,
	//	Operator.And => expr,
	//	Operator.Or => expr,
	//	Operator.Equals => expr,
	//	Operator.Not => expr,
	//	Operator.Nor => expr,
	//	Operator.LessThanEqual => expr,
	//	Operator.GreaterThanEqual => expr,
	//	Operator.LessThan => expr,
	//	Operator.GreaterThan => expr,
	//	_ => throw new ArgumentOutOfRangeException()
	//};

	//private object Plus(Binary binary) => (Eval(lhs), Eval(rhs)) switch {
	//	(decimal l, decimal r) => l + r,
	//	(string l, string r) => l + r,
	//	_ => throw new InvalidOperationException("Can't add those!")
	//};
}
