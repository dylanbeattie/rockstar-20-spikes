namespace Rockstar.Expressions;

public class Variable(string name) : Expression {
	public string Name => name;
}