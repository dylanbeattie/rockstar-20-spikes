using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rockstar.Generator;

[Generator]
public class Generator : ISourceGenerator {
	private static readonly Dictionary<string, string[]> expressionClassesToGenerate = new() {
		{ "Binary", ["Expr lhs", "Token op", "Expr rhs"] },
		{ "Grouping", ["Expr expr"] },
		{ "String", ["string value"] },
		{ "Number", ["decimal value"] },
		{ "Unary", ["Token op, Expr expr "] }
	};

	public void Execute(GeneratorExecutionContext context) {
		var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken)!;
		var source =
			$$"""
			// <auto-generated/>
			namespace {{mainMethod.ContainingNamespace}};

			using System;
			using Rockstar;
			
			public abstract class Expr {
			
				public abstract T Accept<T>(Visitor<T> visitor);
				public interface Visitor<T> {
						
			""";
		foreach (var c in expressionClassesToGenerate) {
			source += $@"		T Visit({c.Key} expr);" + '\n';
		}

		source += "	}";


		foreach (var c in expressionClassesToGenerate) {
			source +=
				$$"""
				  public class {{c.Key}}({{String.Join(", ", c.Value)}}) : Expr {
				  	public override T Accept<T>(Visitor<T> visitor) => visitor.Visit(this);
				""";
				  foreach(var field in c.Value) {
					  source += "public {"
				  	
				  }
				  """";
		}
		source += "}";
		context.AddSource("Expressions.g.cs", source);
	}

	public void Initialize(GeneratorInitializationContext context) {
		// No initialization required for this one
	}
}