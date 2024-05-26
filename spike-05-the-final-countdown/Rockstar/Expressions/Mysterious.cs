namespace Rockstar.Expressions;

class Mysterious : Expression, IAmTruthy {
	private Mysterious() : base(0, 0) { }
	public static Mysterious Instance = new();
	public bool Truthy => false;
}
