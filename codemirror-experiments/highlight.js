import {styleTags, tags} from "@lezer/highlight"

export const highlighting = styleTags({
  Identifier: tags.name,
  Number: tags.number,
  String: tags.string
})