# rockstar-20-spikes
Spikes and experiments for Rockstar 2.0

##### OK, this is where we got to after spike 2.0:

Using Pegasus ([https://github.com/otac0n/Pegasus](https://github.com/otac0n/Pegasus)), we can:

* Create a grammar
* compile it
* run it

The problem is: the thing that does hot reload in Blazor doesn't/won't detect changes to the .peg file.

Which means you need to rebuild the solution to get Blazor to reflect changes to the grammar... which means hot reload doesnt' exactly cut it.

So, options are:

*  Figure out how to do this (can we watch the .PEG file?)
* Don't use hot reload 😢
* Don't use PEG grammar...
  * writing a recursive descent parser
    * [https://www.craftinginterpreters.com/parsing-expressions.html](https://www.craftinginterpreters.com/parsing-expressions.html)
    * if the parser is written entirely in .NET then hot reload probably works (?)
  * using something like ANTLR to define the grammar
    * ...which probably has the same issues that PEG has, namely getting hot reload to pick up the file changes...?

Considerations here:

* Anything that gives us a grammar we can also use for things like a VS Code language server, CodeMirror syntax highlighting, etc. is probably good 😁
