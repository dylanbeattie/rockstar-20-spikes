using System.Globalization;

namespace Engine;

public class Interpreter {
	public string Run(string program) {
		var parser = new PegExamples.ExpressionParser();
		var result = parser.Parse(program);
		return result.ToString(CultureInfo.InvariantCulture);
	}
}
