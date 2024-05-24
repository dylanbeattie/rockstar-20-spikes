namespace Rockstar;

public abstract class Statement {

	public abstract T Accept<T>(IVisitStatements<T> visitor);
	public abstract void Accept(IVisitStatements visitor);

	public class Output(Expr expr) : Statement {
		public Expr Expr => expr;
		public override T Accept<T>(IVisitStatements<T> visitor) => visitor.Visit(this);
		public override void Accept(IVisitStatements visitor) => visitor.Visit(this);
	}

	public class Expression(Expr expr) : Statement {
		public Expr Expr => expr;
		public override T Accept<T>(IVisitStatements<T> visitor) => visitor.Visit(this);
		public override void Accept(IVisitStatements visitor) => visitor.Visit(this);
	}

}

