using static Rockstar.Expression;

namespace Rockstar;

public abstract class Statement {

	public class Output(Rockstar.Expression expr) : Statement {
		public Rockstar.Expression Expr => expr;
		public override string ToString() => $"output: {expr}";
	}

	public class Number(decimal value) : Statement {
	}

	public class Expression(Rockstar.Expression expr) : Statement {
		public Rockstar.Expression Expr => expr;
	}

	public class Assignment(Variable name, object? value) : Statement {
		public string Name => name.Name;
		public object? Value => value;
	}
}

