namespace Rockstar;

public abstract class Expr {
	public abstract T Accept<T>(IVisitExpressions<T> visitor);

	public class String(string value) : Expr {
		public string Value => value;
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class Number(decimal value) : Expr {
		public decimal Value => value;
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class Unary(Token token, Expr expr) : Expr {
		public Token Token => token;
		public Expr Expr => expr;
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class Null : Expr {
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class True : Expr {
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class False : Expr {
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class Mysterious : Expr {
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class Binary(Expr lhs, Token op, Expr rhs) : Expr {
		public Expr Lhs { get; } = lhs;
		public Token Op { get; } = op;
		public Expr Rhs { get; } = rhs;
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}

	public class Variable(Token token) : Expr {
		public string Name => token.Lexeme;
		public override T Accept<T>(IVisitExpressions<T> visitor) => visitor.Visit(this);
	}
}

