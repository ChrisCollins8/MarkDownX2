#MarkDownX2#

##What is it##
[MarkDownX2](http://www.markdownx2.com "MarkDownX2") is a free, opensource markdown editor for Windows written in C# using .NET. It boasts a number of features which are not common in other markdown editors.

#Updates#

##1/17/2015##
1. Implemented the reading/writing of the view settings as XML.
2. Added a basic update mechanism which acts as a placeholder.


##1/14/2015##
1. Made some changes to the markdown lexer to clean up some issues with HTML formatting when dealing with tags after tabs. Initially tried to highlight tags as HTML despite being indented if inside of another html tag which wasn't indented. This worked but came with a significant performance hit. Opted instead to simply highlight all html tags regardless of beging in a code section or not.
2. Finished the find tool.

**Known Issues**
1. Occasionally seeing misstyling of tabs on large documents. Being treated as a code block where not.

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

