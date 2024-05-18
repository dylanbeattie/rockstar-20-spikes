using System.Linq.Expressions;

namespace Rockstar;

public class Parser(IList<Token> tokens) {

	private int current;

	private bool Match(params TokenType[] types) {
		if (!types.Any(Check)) return false;
		Advance();
		return true;
	}

	private bool Check(TokenType type)
		=> !IsAtEnd() && Peek().Type == type;

	private Token Advance() {
		if (!IsAtEnd()) current++;
		return Previous();
	}

	private bool IsAtEnd() => Peek().Type == TokenType.Eof;
	private Token Peek() => tokens[current];
	private Token Previous() => tokens[current - 1];

	public IEnumerable<Statement> Parse() {
		var program = new List<Statement>();
		while (!IsAtEnd()) program.Add(Statement());
		return program;
	}

	/*
	 * program		-> statement* EOF
	 * statement	-> output
	 * output		-> ("shout" | "scream" | "say" | "whisper") expression
	 * expression	-> STRING
	 */

	// say "HELLO WORLD"
	public Statement Statement() {
		if (Match(TokenType.Output)) return OutputStatement();
		return ExpressionStatement();
	}

	public Statement OutputStatement() {
		return new Statement.Output(Expression());
	}

	public Statement ExpressionStatement() {
		return new Statement.Expression(Expression());
	}

	public Expr Expression() {
		return Literal();
	}

	public Expr Literal() {
		if (Match(TokenType.String)) return new Expr.String(Previous().Literal!.ToString()!);
		throw new Exception("OOPS");
	}

}

