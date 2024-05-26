using NCrunch.Framework;
using Pegasus.Common;
using Shouldly;
using Xunit.Abstractions;

namespace Rockstar.Test;

public class FixtureTests(ITestOutputHelper testOutput) : FixtureBase(testOutput) {
	private static readonly Parser parser = new();

	[Theory]
	[MemberData(nameof(GetFiles))]
	public void RunFile(string filePath) {
		var source = File.ReadAllText(filePath);
		var expect = (File.Exists(filePath + ".out")
			? File.ReadAllText(filePath + ".out")
			: ExtractExpects(filePath));
		expect.ShouldNotBeEmpty();
		try {
			var program = parser.Parse(source);
			testOutput.WriteLine(program.ToString());
			var env = new TestEnvironment();
			var interpreter = new Interpreter(env);
			interpreter.Run(program);
			var result = env.Output;
			result.ShouldBe(expect);
		} catch (Exception ex) {
			var cursor = ex.Data["cursor"] as Cursor;
			if (cursor != default) {
				var line = source.Split('\n')[cursor.Line - 1].TrimEnd('\r');
				testOutput.WriteLine(line);
				testOutput.WriteLine(String.Empty.PadLeft(cursor.Column -1) + "^ error is here!");
			}
			var testProjectFilePath = NCrunchEnvironment.GetOriginalProjectPath();
			var testProjectDirectory = Path.GetDirectoryName(testProjectFilePath);
			var originalRockFilePath = Path.Combine(testProjectDirectory, filePath);
			var ncrunchOutputMessage = $"   at <Rockstar code> in {originalRockFilePath}:line {cursor.Line}";
			testOutput.WriteLine(ncrunchOutputMessage);
			throw;

		}
	}
}
