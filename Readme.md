#MarkDownX2#

##What is it##
[MarkDownX2](http://www.markdownx2.com "MarkDownX2") is a free, opensource markdown editor for Windows written in C# using .NET. It boasts a number of features which are not common in other markdown editors.

#Updates#

##1/4/2015##

1. Added in the toolbar and setup merging with the MDI form so the toolbar can be limited appropriately to what is actually available.
2. Cleaned up a formatting bug.

##Feature List##
* Styling
	* **Bold** (`Ctrl+B`)
	* *Italic* (`Ctrl+I`)
	* Quotes (`Ctrl+Q`)
	* Headings 1-6 (`Ctrl+1`, `Ctrl+2`, `Ctrl+3`, `Ctrl+4`, `Ctrl+5`, `Ctrl+6`
	* Lists (`Ctrl+U`, `Ctrl+Shift+O`)
	* Links (`Ctrl+L`)
* MarkDown Syntax Highlighting
* Html Syntax Highlighting
* Real time Preview
* Customizable Preview Rendering
* Tabbed document interface
* Link tool

##Details##
MarkDownX2 is written in C#. It uses Scintilla to provide syntax highlighting for Markdown but uses a custom Lexer written in C# to handle the highlighting. The lexer has some minimal Html highlighting support to account for the fact that many markdown engines will allow html to be embedded in the markdown.

##What MarkDownX2 is not##
MarkDownX2 is not aimed to be a normal text editor. While it can be used to work on normal txt files it will still process them as MD files.

##Tools##

####Link Tool####
A link tool which generates the standard link format.

####Image Tool####
Insert images from drive.

####Formatting Shortcuts####
Automatic formatting shortcuts so you don't have to remember the syntax.

##HTML Support##
MarkDownX2 offers Html highlighting, auto tag closing if desired and tag matching <div></div>


##Planned Features##
* Spell checking
* Port to Mono (The main issue with this is ScintillaNET. The docking library used can be ported to Mono but has no docking functionality but ScintillaNET will require some work to make Mono compatabile.

##Credits##
* [MarkdownDeep](http://www.toptensoftware.com/markdowndeep/ "MarkdownDeep") - C# Markdown processor generates HTML from markdown.

* [Scintilla](http://www.scintilla.org "Scintilla") - Syntax hihglighting editor library written in C++.

* [ScintillaNET](https://scintillanet.codeplex.com/ "ScintillaNET") - C# Wrapper for Scintilla

* [DockPanelSuite](http://dockpanelsuite.com/ "DockPanelSuite") - C# Docking library used to provide the docking interface.

