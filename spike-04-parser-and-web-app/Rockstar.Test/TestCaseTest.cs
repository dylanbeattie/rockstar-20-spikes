using Shouldly;

namespace Rockstar.Test;

public class TestCaseTest {
	[Fact]
	public void ExpectationExtractorExtractsExpectation() {
		var expect = Fixtures.ExtractExpects("""
		                                     shout "hello" (expect: hello)
		                                     shout "world" (expect: world)
		                                     """);
		expect.ShouldBe("""
		                hello
		                world
		                """);
	}
}
