using System.Globalization;

namespace Rockstar;

public abstract class Expression {

}

public class Strïng(string value) : Expression {
	public string Value => value;
}

public class Number(decimal value) : Expression {
	public decimal Value => value;
	public override string ToString() => value.ToString(CultureInfo.InvariantCulture);
}

public class Unary(Token token, Expression expr) : Expression {
	public Token Token => token;
	public Expression Expr => expr;
}

public class Null : Expression {
	
}

public class True : Expression {
	
}

public class False : Expression {
	
}

public class Binary(Expression lhs, Token op, Expression rhs) : Expression {
	public Expression Lhs { get; } = lhs;
	public Token Op { get; } = op;
	public Expression Rhs { get; } = rhs;
}

public class Variable(Token token) : Expression {
	public string Name => token.Lexeme;
}

