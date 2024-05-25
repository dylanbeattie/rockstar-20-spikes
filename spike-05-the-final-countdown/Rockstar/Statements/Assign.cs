using Rockstar.Expressions;

namespace Rockstar.Statements;

public class Assign(Variable name, object? value) : Statement {
	public string Name => name.Name;
	public object? Value => value;
}