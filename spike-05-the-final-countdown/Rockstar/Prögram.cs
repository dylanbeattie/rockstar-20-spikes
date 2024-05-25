namespace Rockstar;

public class Prögram {
	private readonly List<Statement> statements = [];
	public List<Statement> Statements => statements;

	public Prögram Insert(Statement statement) {
		statements.Insert(0, statement);
		return this;
	}

	public Prögram() { }

	public Prögram(Statement statement) => this.statements = [statement];
}
