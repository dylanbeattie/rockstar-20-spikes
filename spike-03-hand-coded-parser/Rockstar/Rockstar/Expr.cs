namespace Rockstar;

public abstract class Expr {
	public abstract T Accept<T>(IVisitor<T> visitor);

	public interface IVisitor<out T> {
		T Visit(Binary expr);
		T Visit(Grouping expr);
		T Visit(Literal expr);
		T Visit(Unary expr);
	}

	public class Binary(Expr lhs, Token op, Expr rhs) : Expr {
		public Expr Lhs => lhs;
		public Token Op => op;
		public Expr Rhs => rhs;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
	public class Grouping(Expr expr) : Expr {
		public Expr Expr => expr;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}

	public class Literal(object? value) : Expr {
		public object? Value => value;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}

	public class Unary(Token op, Expr expr) : Expr {
		public Token Op => op;
		public Expr Expr => expr;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
}
