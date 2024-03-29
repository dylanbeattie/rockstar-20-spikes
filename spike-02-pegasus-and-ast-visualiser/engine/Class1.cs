using System.Text;

namespace engine;

public class Parser {
	public static Node Parse(string expr) {
		var parser = new PegExamples.ExpressionParser();
		var result = parser.Parse(expr);
		return result;
	}
}

public abstract class Node {
	public override string ToString() => this.ToString("");

	public virtual string ToString(string indent)
		=> indent + " - " + this.GetType().Name;
}

public class BinaryOp(Node left, Node right) : Node {
	public override string ToString(string indent) {
		var sb = new StringBuilder();
		sb.Append(indent);
		sb.AppendLine(base.ToString(indent));
		sb.AppendLine(Left.ToString(" " + indent));
		sb.Append(Right.ToString(" " + indent));
		return sb.ToString();
	}
	public Node Left { get; set; } = left;
	public Node Right { get; set; } = right;
}

public class Add(Node left, Node right) : BinaryOp(left,right) { }
public class Sub(Node left, Node right) : BinaryOp(left,right) { }
public class Mul(Node left, Node right) : BinaryOp(left,right) { }
public class Div(Node left, Node right) : BinaryOp(left,right) { }
public class Pow(Node left, Node right) : BinaryOp(left,right) { }

public class Num(decimal value) : Node {
	public decimal Value { get; } = value;
	public override string ToString(string indent)
		=> $"   {indent}- Num: {value}";
}
