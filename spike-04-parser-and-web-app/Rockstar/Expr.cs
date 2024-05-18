namespace Rockstar;

public abstract class Expr {
	public abstract T Accept<T>(IVisitor<T> visitor);

	public class String(string value) : Expr {
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
}

