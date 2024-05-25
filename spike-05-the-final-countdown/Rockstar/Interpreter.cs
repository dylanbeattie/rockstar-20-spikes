using System.Diagnostics;
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
		env.SetVariable(assign.Name, assign.Value);
		return Result.Ok;
	}

	private Result Output(Output output) {
		env.WriteLine(Eval(output.Expr).ToString());
		return Result.Ok;
	}

	private object Eval(Expression expr) => expr switch {
		Number number => number.Value,
		Strïng strïng => strïng.Value,
		Variable v => env.GetVariable(v.Name)!,
		Unary u => u switch {
			{ Op: Operator.Minus, Expr: Number n } => -(n.Value),
			_ => throw new NotImplementedException()
		},
		_ => throw new NotImplementedException()
	};
}
