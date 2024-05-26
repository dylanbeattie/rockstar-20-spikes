using System.Data.Common;
using System.Text;

namespace Rockstar.Expressions;

public abstract class Expression(int line, int column, string? lexeme = default) {

	public virtual void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine(this.GetType().Name.ToLowerInvariant());

	protected string Location => lexeme == default
		? $"(line {line}, column {column})"
		: $"(line {line}, column {column - lexeme.Length}, lexeme {lexeme})";
}