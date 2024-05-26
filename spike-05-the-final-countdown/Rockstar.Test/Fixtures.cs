using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Pegasus.Common;
using Xunit.Abstractions;

namespace Rockstar.Test;

public abstract class FixtureBase(ITestOutputHelper testOutput) {
	public class TestEnvironment : IAmARockstarEnvironment {

		private readonly Dictionary<string, object?> variables = new();
		public void SetVariable(string name, object? value) => variables[name] = value;
		public object? GetVariable(string name) => variables[name];

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
			.Where(filePath => !String.IsNullOrWhiteSpace(ExtractExpects(filePath)))
			.Select(filePath => new[] { filePath });

	public static string ExtractExpects(string filePathOrSourceCode) {
		if (File.Exists(filePathOrSourceCode + ".out")) {
			return File.ReadAllText(filePathOrSourceCode + ".out");
		}

		if (File.Exists(filePathOrSourceCode))
			filePathOrSourceCode = File.ReadAllText(filePathOrSourceCode);

		var tokens = (" " + filePathOrSourceCode).Split("(expect: ");
		return String
			.Join("", tokens
				.Skip(1)
				.Select(e
					=> Regex.Unescape(e.Split(")").First())));
	}
}