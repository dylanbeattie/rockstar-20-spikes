namespace Rockstar;

public class Token(TokenType type, string lexeme, object? literal, int line) {
	public TokenType Type => type;
	public object? Literal => literal;
	public string Lexeme => lexeme;
	public int Line => line;
	public override string ToString() => $"{type} {lexeme} {literal}";
}

public class Parser(List<Token> tokens) {
	private int current;
	private Expr Expression() => Equality();

	private Expr Equality() {
		Expr expr = comparison();
		while (match(TokenType.DoubleEquals, TokenType.NotEqual)) {
			Token op = previous();
			Expr rhs = comparison();
			expr = new Expr.Binary(expr, op, rhs);
		}

		return expr;
	}

	private Expr comparison() {
		Expr expr = term();

		while (match(TokenType.GreaterThan, TokenType.GreaterThanEquals, TokenType.LessThan, TokenType.LessThanEquals)) {
			Token op = previous();
			Expr right = term();
			expr = new Expr.Binary(expr, op, right);
		}

		return expr;
	}

	private Expr term() {
		Expr expr = factor();

		while (match(TokenType.Minus, TokenType.Plus)) {
			Token op = previous();
			Expr right = factor();
			expr = new Expr.Binary(expr, op, right);
		}

		return expr;
	}

	private Expr unary() {
		if (match(TokenType.UnaryNot, TokenType.Minus)) {
			Token op = previous();
			Expr right = unary();
			return new Expr.Unary(op, right);
		}

		return primary();
	}

	private Expr primary() {
		if (match(TokenType.False)) return new Expr.Literal(false);
		if (match(TokenType.True)) return new Expr.Literal(true);
		if (match(TokenType.Nil)) return new Expr.Literal(null);

		if (match(TokenType.Number, TokenType.String)) {
			return new Expr.Literal(previous().Lexeme);
		}

		if (match(TokenType.LEFT_PAREN)) {
			Expr expr = expression();
			consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
			return new Expr.Grouping(expr);
		}
	}

	private Expr factor() {
		Expr expr = unary();
		while (match(TokenType.Divide, TokenType.Times)) {
			Token op = previous();
			Expr right = unary();
			expr = new Expr.Binary(expr, op, right);
		}

		return expr;
	}

	private bool match(params TokenType[] types) {
		if (!types.Any(check)) return false;
		advance();
		return true;
	}

	private bool check(TokenType type) {
		if (isAtEnd()) return false;
		return peek().Type == type;
	}

	private Token advance() {
		if (!isAtEnd()) current++;
		return previous();
	}

	private bool isAtEnd() => peek().Type == TokenType.Eof;

	private Token peek() => tokens[current];

	private Token previous() => tokens[current - 1];

	private Token consume(TokenType type, String message) {
		if (check(type)) return advance();

		throw error(peek(), message);
	}

	private ParseError error(Token token, String message) {
		Lox.error(token, message);
		return new ParseError();
	}

	static void error(Token token, String message) {
		if (token.Type == TokenType.EOF) {
			report(token.line, " at end", message);
		} else {
			report(token.line, " at '" + token.lexeme + "'", message);
		}
	}
	private class ParseError : Exception { }

}

