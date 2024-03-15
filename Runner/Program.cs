var program = File.ReadAllText(args[0]);
var engine = new Engine.Engine();
var result = engine.Run(program);
Console.WriteLine(result);
