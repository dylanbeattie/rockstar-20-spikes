using Newtonsoft.Json.Linq;
using Shouldly;

namespace Rockstar.Test;

public class TestCaseTest {
	[Fact]
	public void ExpectationExtractorExtractsExpectation() {
		var expect = FixtureBase.ExtractExpects("""
		                                     shout "hello" (expect: hello\n)
		                                     shout "world" (expect: world\n)
		                                     """);
		expect.ShouldBe("hello\nworld\n");
	}
}
