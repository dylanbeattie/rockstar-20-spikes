//using System.Linq.Expressions;

//namespace Rockstar;

//public class Parser(IList<Token> tokens) {

//	private int current;

//	private bool Match(params TokenType[] types) {
//		if (!types.Any(Check)) return false;
//		Advance();
//		return true;
//	}

//	private bool Check(TokenType type)
//		=> !IsAtEnd() && Peek().Type == type;

//	private Token Advance() {
//		if (!IsAtEnd()) current++;
//		return Previous();
//	}

//	private bool IsAtEnd() => Peek().Type == TokenType.Eof;
//	private Token Peek() => tokens[current];
//	private Token Previous() => tokens[current - 1];

//	public IEnumerable<Statement> Parse() {
//		var program = new List<Statement>();
//		while (!IsAtEnd()) program.Add(Statement());
//		return program;
//	}

//	/*
//	 * program		-> statement* EOF
//	 * statement	-> output
//	 * output		-> ("shout" | "scream" | "say" | "whisper") expression
//	 * expression	-> STRING | TRUE | FALSE | NULL
//	 */

//	// say "HELLO WORLD"
//	public Statement Statement() {
//		if (Match(TokenType.Output)) return OutputStatement();

//		return ExpressionStatement();
//	}

//	public Statement OutputStatement() {
//		return new Statement.Output(Expression());
//	}

//	public Statement ExpressionStatement() {
//		return new Statement.Expression(Expression());
//	}

//	public Expr Expression() {
//		return Assignment();
//	}

//	public Expr Assignment() {
//		var target = Equality();
//		if (!Match(TokenType.AreEqual) || target is not Expr.Variable variable) return target;
//		var equals = Previous();
//		var value = Assignment();
//		return new Expr.Assign(variable, value);
//	}

//	public Expr Equality() {
//		var expr = Comparison();
//		while (Match(TokenType.AreEqual, TokenType.NotEqual)) {
//			expr = new Expr.Binary(expr, Previous(), Comparison());
//		}
//		return expr;
//	}

//	private static readonly TokenType[] comparators = [
//		TokenType.GreaterThan,
//		TokenType.LessThan,
//		TokenType.GreaterThanEqual,
//		TokenType.LessThanEqual
//	];

//	public Expr Comparison() {
//		var expr = Addition();
//		while (Match(comparators)) {
//			expr = new Expr.Binary(expr, Previous(), Addition());
//		}
//		return expr;
//	}

//	public Expr Addition() {
//		var lhs = Multiplication();
//		while (Match(TokenType.Minus, TokenType.Plus)) {
//			var op = Previous();
//			var rhs = Multiplication();
//			lhs = new Expr.Binary(lhs, op, rhs);
//		}

//		return lhs;
//	}
//	public Expr Multiplication() {
//		var lhs = Unary();
//		while (Match(TokenType.Slash, TokenType.Star)) {
//			var op = Previous();
//			var rhs = Multiplication();
//			lhs = new Expr.Binary(lhs, op, rhs);
//		}
//		return lhs;
//	}

//	public Expr Unary() {
//		while (Match(TokenType.Minus)) {
//			var op = Previous();
//			var rhs = Unary();
//			return new Expr.Unary(op, rhs);
//		}
//		return Literal();
//	}

//	public Expr Literal() {
//		if (Match(TokenType.Mysterious)) return new Expr.Mysterious();
//		if (Match(TokenType.Null)) return new Expr.Null();
//		if (Match(TokenType.True)) return new Expr.True();
//		if (Match(TokenType.False)) return new Expr.False();
//		if (Match(TokenType.String)) return new Expr.String(Previous().Literal!.ToString()!);
//		if (Match(TokenType.Number)) return new Expr.Number((decimal) Previous().Literal!);
//		if (Match(TokenType.Identifier)) return new Expr.Variable(Previous());
//		throw new NotImplementedException($"Literal(): no match for {Peek().Type} {Peek().Lexeme}");
//	}
//}

