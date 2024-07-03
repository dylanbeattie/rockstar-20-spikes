import { nodeResolve } from "@rollup/plugin-node-resolve"

import { lezer } from "@lezer/generator/rollup"

export default [
	 {
		external: ["@lezer/lr", "@lezer/highlight"],
	 	input: "./rockstar.grammar",
	 	output: [
	 		{
	 			format: "es",
	 			file: "./parser.js",
				globals: {
					'@lezer/lr': 'lr',
					'@lezer/highlight': 'highlight',
					'@codemirror/language': 'language'
				}
	 		}],

	 	plugins: [lezer()]
	},
	{
		input: "./editor.mjs",
		output: {
			file: "./dist/editor.bundle.js",
			format: "iife"
		},
		external: ["@lezer/lr", "@lezer/highlight"],
		plugins: [lezer(), nodeResolve()]
	}
]