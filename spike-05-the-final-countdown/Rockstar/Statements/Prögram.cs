using System.Text;

namespace Rockstar.Statements;

public class Prögram {
	private readonly List<Statement> statements = [];
	public List<Statement> Statements => statements;

	public Prögram Insert(Statement statement) {
		statements.Insert(0, statement);
		return this;
	}

	public Prögram() { }

	public Prögram(Statement statement) => statements = [statement];
	public override string ToString() {
		var sb = new StringBuilder();
		foreach (var stmt in Statements) sb.AppendLine(stmt.ToString());
		return sb.ToString();
	}
}
