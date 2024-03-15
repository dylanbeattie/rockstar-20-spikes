var program = File.ReadAllText(args[0]);
var engine = new Engine.Interpreter();
var result = engine.Run(program);
Console.WriteLine(result);
