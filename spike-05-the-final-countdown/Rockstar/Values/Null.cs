using Rockstar.Expressions;

namespace Rockstar.Values;

public class Null(Source source)
	: Value(source) {
	public override bool Truthy => false;
}