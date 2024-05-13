
public class Token(TokenType type, string lexeme, object? literal, int line) {
	public TokenType Type => type;
	public override string ToString() {
		return $"{type} {lexeme} {literal}";
	}
}
