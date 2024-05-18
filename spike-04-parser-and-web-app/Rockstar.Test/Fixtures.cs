using System.Text;
using System.Text.RegularExpressions;
using Shouldly;

namespace Rockstar.Test;

public abstract class FixtureBase {
	public class TestEnvironment : IAmARockstarEnvironment {
		private readonly StringBuilder outputStringBuilder = new();
		public string Output => outputStringBuilder.ToString();
		public string? ReadInput() => null;

		public void WriteLine(string output)
			=> this.outputStringBuilder.Append(output + '\n');

		public void Write(string output)
			=> this.outputStringBuilder.Append(output);
	}

	private static string[] ListRockFiles() =>
		Directory.GetFiles("fixtures", "*.rock", SearchOption.AllDirectories);

	public static IEnumerable<object[]> GetFiles()
		=> ListRockFiles().Select(filePath => new[] { filePath });

	public static IEnumerable<object[]> GetFilesWithExpectations()
		=> ListRockFiles()
			.Where(filePath => File.ReadAllText(filePath).Contains("(expect: "))
			.Select(filePath => new[] { filePath });

	public static string ExtractExpects(string source) =>
		String
			.Join("", source
				.Split("(expect: ")
				.Skip(1)
				.Select(e
					=> Regex.Unescape(e.Split(")").First())));
}

public class FixturePreTests : FixtureBase {
	[Theory]
	[MemberData(nameof(GetFiles))]
	public void FileHasExpectations(string filePath) {
		var source = File.ReadAllText(filePath);
		var expect = ExtractExpects(source);
		expect.ShouldNotBeEmpty();
	}
}

public class FixtureTests : FixtureBase {
	[Theory]
	[MemberData(nameof(GetFilesWithExpectations))]
	public void RunFile(string filePath) {
		var source = File.ReadAllText(filePath);
		var expect = ExtractExpects(source);
		expect.ShouldNotBeEmpty();
		var scanner = new Scanner(source, (_, _) => { });
		var parser = new Parser(scanner.Tokens.ToList());
		var program = parser.Parse();
		var env = new TestEnvironment();
		var interpreter = new Interpreter(env);
		interpreter.Run(program);
		var result = env.Output;
		result.ShouldBe(expect);
	}
}