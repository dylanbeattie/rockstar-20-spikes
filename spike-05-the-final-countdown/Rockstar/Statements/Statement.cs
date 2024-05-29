using System.Text;

namespace Rockstar.Statements;

public abstract class Statement(Source source) {
	public abstract void Print(StringBuilder sb, int depth = 0);
	protected string Location => source.Location;
}

