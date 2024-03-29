using engine;
var expr = args[0];
Console.WriteLine(expr);
var result = Parser.Parse(expr);
Console.WriteLine(result);