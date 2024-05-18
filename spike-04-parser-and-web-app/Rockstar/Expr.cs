namespace Rockstar;

public abstract class Expr {
	public abstract T Accept<T>(IVisitor<T> visitor);

	public class String(string value) : Expr {
		public string Value => value;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}

	public class Number(decimal value) : Expr {
		public decimal Value => value;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}

	public class Unary(TokenType type, Expr expr) : Expr {
		public TokenType Type => type;
		public Expr Expr => expr;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
}

