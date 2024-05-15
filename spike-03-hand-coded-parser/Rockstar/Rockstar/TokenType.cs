using System.Linq.Expressions;

namespace Rockstar;

public enum TokenType {
	Identifier = 0,
	Print, Number, Plus,
	Times, Minus, Divide, Eof,
	Equals,
	DoubleEquals,
	LessThan,
	LessThanEquals,
	GreaterThan,
	GreaterThanEquals,
	UnaryNot,
	NotEqual,
	Slash,
	String,
}