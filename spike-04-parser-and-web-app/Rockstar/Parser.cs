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
	 * expression	-> STRING | TRUE | FALSE | NULL
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
		var lhs = Factor();
		while (Match(TokenType.Minus, TokenType.Plus)) {
			var op = Previous();
			var rhs = Factor();
			lhs = new Expr.Binary(lhs, op, rhs);
		}

		return lhs;
	}
	public Expr Factor() {
		var lhs = Unary();
		while (Match(TokenType.Slash, TokenType.Star)) {
			var op = Previous();
			var rhs = Factor();
			lhs = new Expr.Binary(lhs, op, rhs);
		}
		return lhs;
	}

	public Expr Unary() {
		while (Match(TokenType.Minus)) {
			var op = Previous();
			var rhs = Unary();
			return new Expr.Unary(op, rhs);
		}
		return Literal();
	}

	public Expr Literal() {
		if (Match(TokenType.Mysterious)) return new Expr.Mysterious();
		if (Match(TokenType.Null)) return new Expr.Null();
		if (Match(TokenType.True)) return new Expr.True();
		if (Match(TokenType.False)) return new Expr.False();
		if (Match(TokenType.String)) return new Expr.String(Previous().Literal!.ToString()!);
		if (Match(TokenType.Number)) return new Expr.Number((decimal) Previous().Literal!);
		throw new NotImplementedException($"Literal(): no match for {Peek().Type} {Peek().Lexeme}");
	}
}

