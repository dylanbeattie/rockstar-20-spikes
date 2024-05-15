namespace Rockstar;

public abstract class ExprNode {
	public abstract T Accept<T>(IVisitor<T> visitor);

	public interface IVisitor<out T> {
		T Visit(BinaryNode expr);
		T Visit(GroupingNode expr);
		T Visit(StringNode expr);
		T Visit(NumberNode expr);
		T Visit(UnaryNode expr);
	}

	public class BinaryNode(ExprNode lhs, Token op, ExprNode rhs) : ExprNode {
		public ExprNode Lhs => lhs;
		public Token Op => op;
		public ExprNode Rhs => rhs;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
	public class GroupingNode(ExprNode expr) : ExprNode {
		public ExprNode Expr => expr;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
	public class StringNode(string value) : ExprNode {
		public string Value => value;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
	public class NumberNode(decimal value) : ExprNode {
		public decimal Value => value;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
	public class UnaryNode(Token op, ExprNode expr) : ExprNode {
		public Token Op => op;
		public ExprNode Expr => expr;
		public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
	}
}
