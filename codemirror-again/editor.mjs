import {EditorView, basicSetup} from "codemirror"
import {Rockstar} from "./rockstar.js"

let editor = new EditorView({
  extensions: [basicSetup, Rockstar()],
  parent: document.body
})