namespace Rockstar.Test {
	public class Fixtures {

		public static IEnumerable<object[]> GetFiles() =>
			Directory.GetFiles("fixtures", "*.rock", SearchOption.AllDirectories)
				.Select(filePath => new[] { filePath });

		[Theory]
		[MemberData(nameof(GetFiles))]
		public void RunFile(string filePath) {
			Console.WriteLine(filePath);
		}
	}
}