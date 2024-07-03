import { nodeResolve } from "@rollup/plugin-node-resolve"
import { lezer } from "@lezer/generator/rollup"
export default [
	// {
	// 	input: 'example.grammar',
	// 	output: 'parser.js',
	// 	plugins: [lezer()]
	// },
	{
		input: "./editor.mjs",
		output: {
			file: "./dist/editor.bundle.js",
			format: "iife"
		},
		plugins: [lezer(), nodeResolve()]
	}
]