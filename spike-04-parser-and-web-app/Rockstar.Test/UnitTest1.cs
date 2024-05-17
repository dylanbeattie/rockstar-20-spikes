using System.Text;
using Shouldly;

namespace Rockstar.Test {
	public class Fixtures {
		public class TestEnvironment : IAmARockstarEnvironment {
			private readonly StringBuilder outputStringBuilder = new();
			public string Output => outputStringBuilder.ToString();
			public string? ReadInput() => null;
			public void WriteOutput(string output) => this.outputStringBuilder.Append(output);
		}

		public static IEnumerable<object[]> GetFiles() =>
			Directory.GetFiles("fixtures", "*.rock", SearchOption.AllDirectories)
				.Select(filePath => new[] { filePath });

		[Theory]
		[MemberData(nameof(GetFiles))]
		public void RunFile(string filePath) {
			var source = File.ReadAllText(filePath);
			var expect = ExtractExpects(source);
			var scanner = new Scanner(source);
			var parser = new Parser();
			var ast = parser.Parse(scanner.Tokens);
			var env = new TestEnvironment();
			var interpreter = new Interpreter(env);
			interpreter.Run(ast);
			var result = env.Output;
			result.ShouldBe(expect);
		}

		public static string ExtractExpects(string source) =>
			String
				.Join(Environment.NewLine, source
					.Split("(expect: ")
					.Skip(1)
					.Select(e => e.Split(")")
					.FirstOrDefault()));
	}
}