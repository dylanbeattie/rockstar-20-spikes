using System.Text;

namespace Rockstar.Statements;

public abstract class Statement {
	public abstract void Print(StringBuilder sb, int depth = 0);

}

