namespace Rockstar;

public abstract class Statement {

	public abstract T Accept<T>(IVisitor<T> visitor);

	public class Output(Expr expr) : Statement {
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}

	public class Expression(Expr expr) : Statement {
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}

}

