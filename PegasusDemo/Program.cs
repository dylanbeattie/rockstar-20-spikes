// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var parser = new PegExamples.ExpressionParser();
while(true) {
	Console.Write("> ");
	var source = Console.ReadLine();
	if (source != "") {
		var result = parser.Parse(source);
		Console.WriteLine(result.ToString());
	}
}