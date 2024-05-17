namespace Rockstar;

public static class CharExtensions {
	public static bool IsDigit(this char c) => c is >= '0' and <= '9';
	public static bool IsAlpha(this char c) => c is >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '_';
	public static bool IsAlphaNumeric(this char c) => c.IsDigit() || c.IsAlpha();
}
