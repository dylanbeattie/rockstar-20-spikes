// import { parser } from "./parser.js"
import { parser } from "example.grammar"
import { LRLanguage, LanguageSupport, foldNodeProp, foldInside, indentNodeProp } from "@codemirror/language"
import { styleTags, tags as t } from "@lezer/highlight"
import { completeFromList } from "@codemirror/autocomplete"

let parserWithMetadata = parser.configure({
	props: [
		styleTags({
			Identifier: t.variableName,
			Boolean: t.bool,
			String: t.string,
			LineComment: t.lineComment,
			"( )": t.paren
		}),
		indentNodeProp.add({
			Application: context => context.column(context.node.from) + context.unit
		}),
		foldNodeProp.add({
			Application: foldInside
		})
	]
})

export const RockstarLanguage = LRLanguage.define({
	parser: parserWithMetadata,
	languageData: {
		commentTokens: { line: ";" }
	}
})

const dylanTestKeywords = RockstarLanguage.data.of({
	autocomplete: completeFromList([
		{ label: "defun", type: "keyword" },
		{ label: "defvar", type: "keyword" },
		{ label: "let", type: "keyword" },
		{ label: "cons", type: "function" },
		{ label: "car", type: "function" },
		{ label: "cdr", type: "function" }
	])
});

export function Rockstar() {
	return new LanguageSupport(RockstarLanguage, [dylanTestKeywords])
}
