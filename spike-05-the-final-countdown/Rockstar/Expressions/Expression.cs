using System.Text;

namespace Rockstar.Expressions;

public abstract class Expression {
	public abstract void Print(StringBuilder sb, int depth);
}
