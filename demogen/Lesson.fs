﻿module Lesson

open Script

let nothing =
    [ Launch
      Setup ["Test"; "01234567890123456789"; "Line 3"]
      SetFileType "text"
      Start "Just Testing"
      Insert
      Pause 20000
      Finish ]

let tracey = // first ever test of the system
    [ Launch
      Start "Hey Tracey"
      Move BottomOfDocument
      Move Up
      ZoomMiddle
      Pause 500
      Insert
      Say "Okay..."
      SayWhile ("This is a test", Text "This is a test")
      Pause 100
      Enter
      Pause 100
      SayWhile ("And another", Text "This is another...")
      Esc
      Pause 500
      Say "Let's undo that..."
      Move Up
      SayWhile ("... with two delete line", Normal ("2dd", "two delete line"))
      OpenAbove
      Esc
      Insert
      SayWhile ("Hey Tracey!", Text "I like you!")
      Esc
      Say "Now watch this"
      Pause 500
      SayWhile ("Back two words", Normal ("2b", "two back"))
      SayWhile ("Change word", Normal ("cw", "change word"))
      Text "really love"
      Esc
      Finish ]

let dontRepeat = // using dot
    [ Launch
      Setup ["var foo = 1"; "var bar = 'a'"; "var foobar = foo + bar"]
      Start "Don't Repeat Yourself"
      Say "Let's say that we want to add semicolons after each line."
      SayWhile ("We could use dollar to move to the end of the first line.", Move EndOfLine)
      SayWhile ("Then A for after.", After)
      SayWhile ("Type a semicolon", Text ";")
      SayWhile ("Then escape to return to normal mode.", Esc)
      Say "This is a little tedious"
      SayWhile ("Down a line", Move Down)
      SayWhile ("And do it again. End of line.", Move EndOfLine)
      SayWhile ("After.", After)
      SayWhile ("Semicolon.", Text ";")
      SayWhile ("And escape.", Esc)
      Say "And once more."
      SayWhile ("Down", Move Down)
      SayWhile ("End", Move EndOfLine)
      SayWhile ("After.", After)
      SayWhile ("Semicolon.", Text ";")
      SayWhile ("And one last escape.", Esc)
      Say "This has taken us 14 keystrokes and a lot of repetition."
      Say "Let's see if there's a better way."
      Say "Undoing all of that."
      SayWhile ("Undo.", Undo)
      SayWhile ("Undo.", Undo)
      SayWhile ("Undo.", Undo)
      SayWhile ("And back to the start.", Move FirstColumn)
      SayWhile ("Instead let's use Shift-A to insert after the line in a single action.", AfterLine)
      SayWhile ("Type a semicolon", Text ";")
      SayWhile ("Then escape to normal mode.", Esc)
      Say "Best of all, this whole action can be replayed with the period or 'dot' key."
      SayWhile ("Down a line", Move Down)
      SayWhile ("Then just press dot to repeat.", Repeat)
      Say "Isn't that cool?"
      SayWhile ("Then down again", Move Down)
      SayWhile ("A repeat once more", Repeat)
      Say "And we're done, in just seven keystrokes."
      Say "It's important to note that dot remembers a single action, so using Shift-A instead of dollar followed by A, not only saves a keystroke, but makes it repeatable with dot."
      Say "Still, even down, dot, down, dot, can be repetitive with many lines."
      SayWhile ("Let's undo all of this again.", Compound (150, [Undo; Undo; Undo; Move FirstColumn]))
      SayWhile ("And let's make more lines; yanking to the last line.", YankToLastLine)
      SayWhile ("Then put it back several times.", PutBefore)
      SayWhile ("Put.", PutBefore)
      SayWhile ("Put.", PutBefore)
      SayWhile ("Put.", PutBefore)
      Say "Again, let's add semicolons after each line, but this time we'll recored a macro."
      SayWhile ("Press Q Q to record. Q is the record key and I like to use Q as a quick temporary macro register.", Record 'q')
      SayWhile ("Then do Shift-A to insert after the line as before.", AfterLine)
      SayWhile ("Add our semicolon", Text ";")
      SayWhile ("And escape.", Esc)
      SayWhile ("And down a line, while still recording.", Move Down)
      SayWhile ("And finally, stop recording.", StopRecording)
      SayWhile ("If we play the macro back now with at Q, it adds the semicolon and advances to the next line.", Macro 'q')
      SayWhile ("We can press at, at to repeat again.", RepeatLastMacro)
      SayWhile ("And again.", RepeatLastMacro)
      Say "Or we can repeat the macro the remaining 11 times in one shot!"
      SayWhile ("With eleven at !", RepeatMacro ('q', 11))
      Say "We've made pretty quick work of that with the power of Vim!"
      Finish ]

let numbers = // numbering list with `g^a`
    [ Launch
      Start "Numbering Lists"
      Say "Here's a quick trick to create numbered lists."
      Insert
      Text "Milk"
      Pause 100
      Enter
      Text "Bread"
      Pause 100
      Enter
      Text "Hot dogs"
      Pause 100
      Enter
      Text "Mustard"
      Pause 100
      Enter
      Text "Jalapenos"
      Esc
      Say "To add numbers, we can go to the first column."
      Move FirstColumn
      Say "And select block with Control-V"
      SelectBlock
      Pause 800
      SayWhile ("And move to the top with G G.", Move TopOfDocument)
      Pause 800
      SayWhile ("Then insert before with Shift-I.", InsertBefore)
      Pause 800
      SayWhile ("And number the line with a zero.", Text "0. ")
      Pause 800
      SayWhile ("Escape then does this to the whole vertical selection.", Esc)
      Say "That's pretty neat by itself. Here's the trick though."
      SayWhile ("We reselect the first column with G V.", Reselect)
      Pause 800
      SayWhile ("And press G followed by Control-A to increment as ordinals.", IncrementOrdinals)
      Say "It automatically increments the whole select in order. Very cool!"
      Finish ]

let alignText = // aligning text with `:norm` commands
    [ Launch
      Setup ["{ first: \"Ash\","; "  last: \"Ford\","; "  height: 1.83,"; "  age: 50,"; "  eyes: \"blue\" }"]
      SetFileType "javascript"
      Start "Align Text"
      Pause 2000
      Say "The property names here are nicely aligned on the left, but the values aren't."
      SayWhile ("It looks like the height is furthest over.", Compound (150, [Move Down; Move Down; Move (Find '1')]))
      Say "We can see that it's at column eleven."
      Say  "Here's the trick."
      SayWhile ("We select the block.", Compound (150, [SelectBlock; AroundBlock]))
      SayWhile ("And enter a normal mode command with colon-norm.", Text ":norm ")
      Say "This will apply normal mode command to every selected line."
      SayWhile ("We'll find the colon...", Text "f:")
      SayWhile ("And insert 4 spaces after it.", Text "4a ")
      Enter
      Say "Look at that. Of course it's too much space for some of them."
      SayWhile ("We'll reselect the block.", Reselect)
      SayWhile ("And with another normal mode command...", Text ":norm ")
      SayWhile ("We can go to column eleven on each line.", Text "11|")
      SayWhile ("And delete word.", Text "dw")
      Enter
      Say "And voila! Applying commands to every line is a great trick."
      Finish ]

let loremIpsum = [
    "Lorem ipsum dolor "; "sit amet, "; "consectetur "; "adipiscing elit, "; "sed do eiusmod "
    "tempor incididunt "; "ut labore et dolore "; "magna aliqua. Ut "; "enim ad minim "
    "veniam, quis "; "nostrud "; "exercitation "; "ullamco laboris "; "nisi ut aliquip ex "
    "ea commodo "; "consequat. Duis "; "aute irure dolor in "; "reprehenderit in "
    "voluptate velit esse "; "cillum dolore eu "; "fugiat nulla "; "pariatur. Excepteur "
    "sint occaecat "; "cupidatat non "; "proident, sunt in "; "culpa qui officia "
    "deserunt mollit "; "anim id est "; "laborum. " ]

let basicMotions1 = // h j k l ␣ ⌫
    [ Launch
      Setup loremIpsum
      Pause 10000
      SetFileType "text"
      Start "Basic Motions 1"
      SayWhile ("In Vim, we can certainly move around with the arrow keys...", Compound (250, [
        Key ("↓", "down arrow", "{Down}")
        Key ("↓", "down arrow", "{Down}")
        Key ("↓", "down arrow", "{Down}")
        Key ("↓", "down arrow", "{Down}")
        Key ("↓", "down arrow", "{Down}")
        Key ("→", "right arrow", "{Right}")
        Key ("→", "right arrow", "{Right}")
        Key ("→", "right arrow", "{Right}")
        Key ("→", "right arrow", "{Right}")
        Key ("→", "right arrow", "{Right}")
        Key ("↑", "up arrow", "{Up}")
        Key ("↑", "up arrow", "{Up}")
        Key ("↑", "up arrow", "{Up}")
        Key ("↑", "up arrow", "{Up}")
        Key ("↑", "up arrow", "{Up}")
        Key ("←", "left arrow", "{Left}")
        Key ("←", "left arrow", "{Left}")
        Key ("←", "left arrow", "{Left}")
        Key ("←", "left arrow", "{Left}")
        Key ("←", "left arrow", "{Left}") ]))
      Say "But the Vim way of doing things is to use the home row keys."
      Pause 500
      SayWhile ("J to move down...", Compound (250, [Move Down; Move Down; Move Down; Move Down; Move Down]))
      Pause 1000
      SayWhile ("L to move right...", Compound (250, [Move Right; Move Right; Move Right; Move Right; Move Right]))
      Pause 1000
      SayWhile ("K to move up...", Compound (250, [Move Up; Move Up; Move Up; Move Up; Move Up]))
      Pause 500
      SayWhile ("and H to move left...", Compound (250, [Move Left; Move Left; Move Left; Move Left; Move Left]))
      Pause 1000
      SayWhile ("You can also move right with space...", Compound (250, [Key ("␣", "space", "l"); Key ("␣", "space", " "); Key ("␣", "space", " "); Key ("␣", "space", " "); Key ("␣", "space", " ")]))
      Pause 1000
      SayWhile ("and move left with backspace.", Compound (250, [Key ("⌫", "backspace", "h"); Key ("⌫", "backspace", "h"); Key ("⌫", "backspace", "h"); Key ("⌫", "backspace", "h"); Key ("⌫", "backspace", "h")]))
      Pause 500
      Say "These are the most basic movements."
      Finish ]

let basicMotions2 = // w b e ge
    [ Launch
      Setup loremIpsum
      Pause 10000
      SetFileType "text"
      Start "Basic Motions 2"
      SayWhile ("In Vim, we can move by words with W...", Compound (400, [Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word]))
      Pause 1000
      SayWhile ("And with B for back by words.", Compound (400, [Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord]))
      Pause 1000
      SayWhile ("Notice that it moves to the beginning of each word.", Compound (800, [Move Word; Move Word; Move Word; Move BackWord; Move BackWord; Move BackWord]))
      Pause 1000
      SayWhile ("Pressing E moves to the end of each word.", Compound (400, [Move WordEnd; Move WordEnd; Move WordEnd; Move WordEnd; Move WordEnd; Move WordEnd; Move WordEnd]))
      Pause 1000
      SayWhile ("And finally, G followed by E moves back to the end of each previous word.", Compound (400, [Move BackWordEnd; Move BackWordEnd; Move BackWordEnd; Move BackWordEnd; Move BackWordEnd; Move BackWordEnd]))
      Pause 500
      SayWhile ("With W...", Compound (400, [Move Word; Move Word]))
      SayWhile ("...B...", Move BackWord)
      SayWhile ("...E...", Move WordEnd)
      SayWhile ("...and G followed by E, we can quickly move by words forward and backward to the start and end of each.", Move BackWordEnd)
      Finish ]

let basicMotions3 = // W B E gE
    [ Launch
      Setup [""; ""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"]
      Pause 10000
      SetFileType "python"
      Start "Basic Motions 3"
      Compound (0, [Move Down; Move Down; Move Down; Move Down; Move Down])
      SayWhile ("You may have noticed that navigating by words with lowercase W stops at punctuation.", Compound (400, [Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word; Move Word]))
      Pause 1000
      SayWhile ("The same happens when moving back by words with B. It stops at each bit of punctuation.", Compound (400, [Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord; Move BackWord]))
      Pause 1000
      Say  "It may be quicker to navigate by so-called 'big words'. Here's how:"
      SayWhile ("Shift W moves forward by big words.", Compound (400, [Move BigWord; Move BigWord; Move BigWord; Move BigWord; Move BigWord; Move BigWord; Move BigWord; Move BigWord]))
      Pause 800
      Say "This treats words as consisting of any non-whitespace characters."
      Pause 800
      SayWhile ("Shift-B moves back by big words.", Compound (800, [Move BackBigWord; Move BackBigWord; Move BackBigWord; Move BackBigWord]))
      Pause 1000
      SayWhile ("Likewise, Shift-E moves to the end of big words.", Compound (800, [Move BigWordEnd; Move BigWordEnd; Move BigWordEnd]))
      Pause 1000
      SayWhile ("And finally G followed by Shift-E, moves to the end of each previous big word.", Compound (800, [Move BackBigWordEnd; Move BackBigWordEnd; Move BackBigWordEnd]))
      Pause 800
      SayWhile ("With Shift-W...", Compound (400, [Move BigWord; Move BigWord]))
      SayWhile ("Shift-B...", Move BackBigWord)
      SayWhile ("Shift-E...", Move BigWordEnd)
      SayWhile("...and G followed by Shift-E...", Move BackBigWordEnd)
      Say "...we can very quickly move by big words."
      Finish ]

let basicMotions4 = // $ ^ 0
    [ Launch
      Setup [""; ""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"]
      Pause 10000
      SetFileType "python"
      Start "Basic Motions 4"
      Compound (0, [Move Down; Move Down])
      Say "Here are some more basic horizontal motions."
      SayWhile ("You can move to the end of a line with dollar.", Move EndOfLine)
      Pause 1000
      SayWhile ("And to the start of a line with carrot.", Move StartOfLine)
      Say "You may recognize dollar and carrot from regular expressions."
      Pause 1000
      SayWhile ("Notice that carrot actually moves to the first non-blank character of a line.", Move Down)
      SayWhile ("Carrot", Move StartOfLine)
      Pause 800
      SayWhile ("Dollar", Move EndOfLine)
      Pause 800
      SayWhile ("Carrot", Move StartOfLine)
      Pause 1000
      Say "To move to the first column even when it's blank..."
      SayWhile ("...press zero.", Move FirstColumn)
      Pause 1000
      Say "To recap..."
      SayWhile ("...dollar moves to the end...", Move EndOfLine)
      Pause 800
      SayWhile ("...carrot moves to the first non-blank character...", Move StartOfLine)
      Pause 800
      SayWhile ("...and zero moves to the first column.", Move FirstColumn)
      Finish ]

let basicMotions5 = // H L M gg G
    [ Launch
      Setup ["def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"; ""; "def fib(n):"; "  if n <= 1:"; "    return n"; "  return fib(n-1) +"; "    fib(n-2)"; ""; "print(fib(7))"]
      Pause 10000
      SetFileType "python"
      Start "Basic Motions 5"
      Compound (0, [Move Down; Move Down])
      Say "Here are a few basic vertical motions."
      SayWhile ("You can move to the lowest line on the screen with Shift-L", Move LowestLine)
      Pause 1000
      SayWhile ("And back to the highest visible line with Shift-H.", Move HighestLine)
      Pause 1000
      SayWhile ("Or to the middle line with Shift-M", Move MiddleLine)
      Pause 1000
      SayWhile ("Notice that, if our cursor is somewhere within a line, the column is maintained.", Move WordEnd)
      Pause 800
      SayWhile ("Highest", Move HighestLine)
      Pause 500
      SayWhile ("Middle", Move MiddleLine)
      Pause 500
      SayWhile ("Lowest", Move LowestLine)
      Pause 600
      Say "These keys work with what's currently visible on the screen."
      SayWhile ("There's more text below...", Compound (400, [Move Down; Move Down; Move Down; Move Down; Move Down]))
      Pause 1000
      Say "We can move to the top of the whole document with..."
      SayWhile ("...GG", Move TopOfDocument)
      Pause 1000
      Say "And back to the bottom of the whole document with..."
      SayWhile ("...Shift-G", Move BottomOfDocument)
      Pause 1000
      Say "To recap..."
      SayWhile ("...Shift-H moves to the highest visible line.", Move HighestLine)
      Pause 800
      SayWhile ("...Shift-M for middle of the screen...", Move MiddleLine)
      Pause 800
      SayWhile ("...and Shift-L for the lowest visible line.", Move LowestLine)
      Pause 800
      SayWhile ("GG takes us to the top of the document...", Move TopOfDocument)
      Pause 800
      SayWhile ("... and Shift-G takes us to the bottom of the document...", Move BottomOfDocument)
      Pause 800
      Finish ]

let basicMotions6 = // ) ( } {
    [ Launch
      Setup ["# It's a sentence.  And again!          And another         one... How          about this? Indeed!"; ""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"; ""; "def fib(n):"; "  if n <= 1:"; "    return n"; "  return fib(n-1) +"; "    (n-2)"; ""; "print(fib(7))"]
      Pause 10000
      SetFileType "python"
      Start "Basic Motions 6"
      Say "We can move back and forth by whole sentences with..."
      SayWhile ("...right parenthesis to move to each next sentence...", Compound (800, [Move Sentence; Move Sentence; Move Sentence; Move Sentence]))
      Pause 1000
      SayWhile ("... and left parenthesis to move back by sentences...", Compound (800, [Move BackSentence; Move BackSentence; Move BackSentence; Move BackSentence]))
      Pause 1000
      SayWhile ("Sentences are terminated by period, exclamation point or question mark, followed by whitespace.", Compound (800, [Move Sentence; Move Sentence; Move Sentence; Move Sentence; Move BackSentence; Move BackSentence; Move BackSentence; Move BackSentence]))
      Pause 1000
      Say "We can also move up an down by paragraphs with..."
      SayWhile ("...right curly brace to move down to each next paragraph...", Compound (800, [Move Paragraph; Move Paragraph; Move Paragraph; Move Paragraph; Move Paragraph]))
      Pause 1000
      SayWhile ("... and left curly brace to move back up by paragraphs", Compound (800, [Move BackParagraph; Move BackParagraph; Move BackParagraph; Move BackParagraph; Move BackParagraph]))
      Pause 1000
      Say "Paragraphs are separated by blank lines."
      Pause 1000
      SayWhile ("Using left and right parenthesis and left and right curly braces is a nice way to quickly navigate by sentences and paragraphs.", Compound (800, [Move Sentence; Move Sentence; Move Sentence; Move Sentence; Move BackSentence; Move BackSentence; Move BackSentence; Move BackSentence; Move Paragraph; Move Paragraph; Move Paragraph; Move Paragraph; Move Paragraph; Move BackParagraph; Move BackParagraph; Move BackParagraph; Move BackParagraph; Move BackParagraph]))
      Pause 800
      Finish ]

let basicMotions7 = // ]] [[ ][ []
    [ Launch
      Setup ["#include <stdio.h>"; ""; "int isEven(int n)"; "{"; "  return n % 2 == 0;"; "}"; ""; "void msg(char *m)"; "{"; "  printf(\"%s\\n\",m);"; "}"; ""; "int main()"; "{"; "  if (!isEven(42))"; "  {"; "    msg(\"Panic!\");"; "  }"; "  return 0;"; "}"; ""]
      Pause 10000
      Text ":set cursorline"
      Enter
      SetFileType "c"
      Start "Basic Motions 7"
      Say "In curly brace languages, it's nice to navigate by sections."
      Pause 800
      SayWhile ("Right bracket bracket moves down to the curly brace at the start of each section.", Compound (800, [Move SectionStart; Move SectionStart; Move SectionStart; Move SectionStart]))
      Say "Stopping at the bottom of the document."
      Pause 800
      SayWhile ("Left bracket bracket moves back up to the start of each section.", Compound (800, [Move BackSectionStart; Move BackSectionStart; Move BackSectionStart; Move BackSectionStart]))
      Say "Stopping at the top of the document."
      Pause 800
      Say "A section is a block of lines surrounded by curly braces in the first column."
      Pause 800
      SayWhile ("Notice that down here...", Compound (200, [Move SectionStart; Move SectionStart; Move SectionStart]))
      SayWhile ("Section navigation doesn't include the if statement because those curly braces are not in the first column.", Compound (800, [Move SectionStart; Move BackSectionStart; Move SectionStart; Move BackSectionStart]))
      Pause 800
      Say "We can also move up to the end, rather than the start, of each section with..."
      SayWhile (".. left-bracket right-bracket.", Compound (800, [Move BackSectionEnd; Move BackSectionEnd; Move BackSectionEnd]))
      Pause 800
      Say "Or move down to the end of each section with..."
      SayWhile (".. right-bracket left-bracket.", Compound (800, [Move SectionEnd; Move SectionEnd; Move SectionEnd]))
      Pause 800
      SayWhile ("Combinations of pairs of brackets make navigation pretty quick.", Compound (200, [Move BackSectionStart; Move BackSectionStart; Move BackSectionStart; Move BackSectionStart; Move SectionEnd; Move SectionEnd; Move SectionEnd; Move SectionEnd; Move BackSectionEnd; Move BackSectionEnd; Move BackSectionEnd; Move BackSectionEnd; Move BackSectionEnd; Move SectionEnd; Move SectionEnd; Move SectionEnd; Move SectionEnd; Move SectionEnd]))
      Finish ]

let basicMotions8 = // + -
    [ Launch
      Setup ["def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"; ""; "def fib(n):"; "  if n <= 1:"; "    return n"; "  return fib(n-1) +"; "    fib(n-2)"; ""; "print(fib(7))"]
      Pause 10000
      SetFileType "python"
      Start "Basic Motions 8"
      SayWhile ("Let's say we're at the end of the first line and we want to move to the start of the next line.", Move EndOfLine)
      Pause 800
      SayWhile ("We could move down with J...", Move Down)
      Pause 800
      SayWhile ("... then to the first non-blank character with carrot.", Move StartOfLine)
      Pause 800
      SayWhile ("And again, down...", Move Down)
      Pause 500
      SayWhile ("... then start of line.", Move StartOfLine)
      Pause 500
      SayWhile ("Down...", Move Down)
      Pause 500
      SayWhile ("... start.", Move StartOfLine)
      Pause 500
      SayWhile ("And the same idea to move up to the start of each line.", Compound (800, [Move Up; Move StartOfLine; Move Up; Move StartOfLine; Move Up; Move StartOfLine]))
      Pause 800
      Say "This is pretty tedious and is common enough that there are single keys for this!"
      Pause 800
      Say "Plus and minus do exactly this."
      Pause 800
      SayWhile ("Plus to move down.", Compound (400, [Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; Move StartOfNextLine; ]))
      Pause 800
      SayWhile ("And minus to move up.", Compound (400, [Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; Move StartOfPreviousLine; ]))
      Finish ]

let matchingPairs = // %
    [ 
      //Launch // launch opens nvim via Ubuntu and matching is broken for some reason (manually launch nvim directly instead)
      Pause 10000
      Setup ["{ x = true"; "  y = [1; 2; 3]"; "  z = ((4, 2),"; "       \"life\")"; "} |> test"; ""; ""; ""]
      Pause 10000
      SetFileType "ocaml"
      Start "Matching Pairs"
      Say "It can be quite handy to quickly jump between matching pairs of braces and brackets."
      Pause 300
      SayWhile ("We can jump between these curly braces by pressing percent.", Compound(500, [Move MatchingBraces; Move MatchingBraces; Move MatchingBraces; Move MatchingBraces; Move MatchingBraces; Move MatchingBraces]))
      Pause 500
      SayWhile ("It works with square brackets too.", Move Down)
      Say "Notice that we don't have to be on the first bracket for it to work."
      Pause 200
      Say "Pressing percent from here at the first column..."
      SayWhile ("...finds the first bracket and jumps to it's pair at the end of the line.", Move MatchingBraces)
      Pause 200
      SayWhile ("The same is true when the cursor is inside the brackets.", Compound (0, [Move Left; Move Left; Move Left; Move Left]))
      Pause 200
      SayWhile ("Pressing percent finds the closing bracket.", Move MatchingBraces);
      Pause 200
      SayWhile ("From there, pressing percent jumps between the pairs.", Compound (500, [Move MatchingBraces; Move MatchingBraces; Move MatchingBraces; Move MatchingBraces]))
      Pause 200
      SayWhile ("Of course, it works with parenthesis as well.", Move Down)
      Pause 200
      Compound(500, [Move MatchingBraces; Move MatchingBraces; Move MatchingBraces; Move MatchingBraces])
      Pause 200
      SayWhile ("And works with nesting.", Move Right)
      Compound(500, [Move MatchingBraces; Move MatchingBraces; Move MatchingBraces; Move MatchingBraces])
      Say "Pretty handy!"
      Finish ]

let uselessUnderscore = // _
    let underscore = Key ("⇧_", "", "_")
    [ Launch
      Setup ["{ first: \"Ash\","; "  last: \"Ford\","; "  height: 1.83,"; "  age: 50,"; "  eyes: \"blue\" }"]
      Pause 10000
      SetFileType "ocaml"
      Start "Useless Underscore"
      SayWhile ("The underscore key is virtually useless.", Compound (0, [Move WordEnd; Move Down]))
      Pause 600
      SayWhile ("When pressed, it does the same thing as the carrot key by moving to the first non-blank character of the current line.", Key ("⇧_", "start of line", "{^}"))
      Pause 600
      SayWhile ("One difference is that carrot doesn't use a count, but underscore does; moving down by n minus 1 lines.", Move BigWordEnd)
      SayWhile ("Three underscore, moves down two lines.", Key ("3⇧_", "start of line", "{+}{+}"))
      Pause 600
      SayWhile("But we already have similar behavior with the plus key", Compound (0, [Move Up; Move Up; Move BigWordEnd]))
      Pause 600
      SayWhile ("Two plus also moves down 2 lines.", Key ("2⇧+", "start of next line", "{+}{+}"))
      Pause 600
      Say "So I don't see a use for underscore that isn't already covered by carrot and plus."
      Pause 400
      Say "We can't let a key go unused though!"
      Pause 400
      SayWhile ("So I personally map it...", Text ":map _ ")
      Pause 400
      SayWhile ("...to insert a space...", Text "i ")
      SayWhile ("...and return to normal mode...", Text "<Esc>")
      Say "You'd be surprised how handy this is!"
      Pause 400
      Enter
      SayWhile ("For example, we can quickly align this while in normal mode.", Compound (100, [Move BigWord]))
      Pause 500
      Compound (200, [underscore; underscore; underscore])
      Pause 800
      Move Down
      Pause 500
      Compound (200, [underscore; underscore])
      Pause 800
      Compound (200, [Move Up; Move Up; Move Up])
      Pause 500
      Compound (200, [underscore; underscore])
      Pause 800
      Compound (200, [Move Up; Move Word])
      Pause 500
      underscore
      Say "Much more useful now!"
      Finish ]

let findCharacter = // f t F T ; ,
    [ Launch
      Setup [""; ""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"; ""; "def fib(n):"; "  if n <= 1:"; "    return n"; "  return fib(n-1) +"; "    fib(n-2)"; ""; "print(fib(7))"]
      Pause 10000
      SetFileType "python"
      Move Paragraph; Move Up
      Start "Find Character"
      Say "A quick way to move around a line is to find characters."
      Pause 400
      Say "To jump to the n variable, we can press F, for 'find', followed by N"
      SayWhile ("...and it finds the first N.", Move (Find 'n'))
      Pause 400
      Say "Oops, that's not the N we wanted."
      Pause 200
      Say "Pressing semicolon finds the next one."
      Move (NextChar 'n')
      Pause 600
      SayWhile ("And again to the next.", Move (NextChar 'n'))
      Pause 600
      Say "To go back, press comma."
      Move (PrevChar 'n')
      Pause 600
      Say "Another way is to jump to just before a character, with T, for 'find till'."
      SayWhile ("From the start of the line...", Move FirstColumn)
      Pause 300
      Say "Press T followed by star..."
      Move (Till '*')
      Pause 300
      Say "Taking us to just before the star, which is directly to the N we want."
      Pause 400
      Say "We can also find in the reverse direction with shift F."
      Pause 600
      SayWhile ("From the end of the line...", Move EndOfLine)
      Pause 600
      Say "We can find the previous N with shift F followed by N."
      Move (FindReverse 'n')
      Pause 600
      SayWhile ("And the one before that with semicolon.", Move (NextChar 'n'))
      Say "Semicolon and comma work relative to the original direction."
      Pause 600
      Say "Like shift F, shift T also works in the reverse direction."
      Say "This is a slick way to move around!"
      Finish ]

let search = // * # n N / ?
    [ Launch
      Setup [""; ""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"; ""; "def fib(n):"; "  if n <= 1:"; "    return n"; "  return fib(n-1) +"; "    fib(n-2)"; ""; "print(fib(7))"]
      Move TopOfDocument
      Move Down
      Move Down
      Move Word
      SetFileType "python"
      Text ":set nohls"; Enter; Text ":"; Esc
      Start "Search"
      Say "When we want to find the word under the cursor, we can simply press star."
      Move SearchStar
      Say "We can move to the next one by pressing N."
      Move SearchNext
      Say "Or we can move back with shift N."
      Move SearchPrev
      Pause 800
      Move SearchPrev
      Pause 800
      Say "Let's try from the bottom of the document."
      Move BottomOfDocument
      Move Word; Move Word
      Say "We can search in reverse for the word under the cursor by pressing the hash key."
      Move SearchHash
      Pause 800
      Say "And again, pressing N takes us to the next ones."
      Move SearchNext
      Pause 800
      Move SearchNext
      Say "And shift N to traverse them in reverse."
      Move SearchPrev
      Pause 400
      Move SearchPrev
      Pause 400
      Move SearchPrev
      Say "That's quite useful!"
      Pause 1000
      Say "Back to the top of the document."
      Move TopOfDocument
      Say "We can also search for anything we like by pressing slash."
      Key ("/", "search", "/")
      Say "And entering a search term"
      Text "fib"
      Say "Pressing enter takes us there."
      Enter
      SayWhile ("Pressing N and shift N cycles through the matches.", Compound (500, [Move SearchNext; Move SearchNext; Move SearchNext; Move SearchPrev; Move SearchPrev; Move SearchPrev]) )
      Pause 1000
      Say "Finally, we can search in reverse by pressing question mark"
      Key ("⇧?", "search", "?")
      Pause 100
      Text "fact"
      Pause 1000
      Enter
      Pause 1000
      SayWhile ("Searching with star and hash, or with slash and question mark, and navigating the results with N and shift N is a powerful way to find our way around!", Compound(800, [Move SearchNext; Move SearchNext; Move SearchNext; Move SearchPrev; Move SearchPrev; Move SearchPrev]))
      Finish ]

let joinLines = // J gJ
    [ Launch
      Pause 5000
      Setup [""; ""; "fruits = ["; "  'lime',"; "  'fig',"; "  'kiwi',"; "  'pear'"; "]";]
      SetFileType "python"
      Move Down; Move Down; Move Down; 
      Start "Join Lines"
      Say "Joining lines is pretty useful. Pressing shift J will join the current line with the one below."
      JoinLine
      Pause 1000
      Say "Notice that it leaves a single space under the cursor."
      Pause 1000
      Move Down
      JoinLine
      Pause 500
      Say "To bring up the final closing bracket, perhaps we don't want a space. For that we can press G followed by shift J"
      Key ("g⇧J", "join line no space", "gJ")
      Pause 1000
      Say "It's surprising how often the J key comes in handy!"
      Finish ]

let quitting = // :q :q! ZQ  :wq :x ZZ  ^z  fg
    [ Launch
      Pause 5000
      Setup [""; ""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"]
      SetFileType "python"
      Pause 3000
      Text ":file fact.py"; Enter
      Text ":w!"; Enter
      Text "ZZ"; Pause 1000; Text "clear"; Enter; Text "vi fact.py"; Enter
      Start "Quitting"
      Say "It's a running joke that people have to power off their computers to exit Vim, but come on it's really not difficult."
      Pause 500
      Say "Simply pressing ZZ will save and quit."
      Key ("⇧ZZ", "save and quit", "ZZ")
      Pause 2000
      Text "vi fa"; Tab; Enter
      Pause 1000
      SayWhile ("Entering the command colon X does the same thing.", Text ":x")
      Text "\b"
      SayWhile ("Or the more verbose colon W Q also writes and quits.", Text "wq")
      Enter
      Pause 1000
      Text "!!"; Enter; Enter
      Pause 1000
      SayWhile ("If we make a change to the file...", Compound (150, [Move Down; Move Down; Move WordEnd; Move WordEnd; After; Text "orial"; Esc]))
      Say "Then we now have a choice. We can save and quit with ZZ, or we can abandon our changes with ZQ"
      Key ("⇧ZQ", "quit without saving", "ZQ")
      Pause 1000
      Text "!!"; Enter; Enter
      Pause 1000
      Say "When be come back, our changes have not been saved."
      SayWhile ("The command colon Q bang also quits without saving.", Text ":q!")
      SayWhile ("Without the bang quits too, but only when there are no changes to be saved.", Text "\b")
      Pause 500
      Esc
      Say "One more way to get out of Vim is to suspend it with control Z"
      Key ("⌃z", "suspend", "^z")
      Pause 1000
      Say "We can resume the process later with F G"
      Text "fg"; Enter
      Finish ]

let revertFile = // :e!
    [ Launch
      Pause 5000
      Setup [""; "def fact(n):"; "  if n == 0:"; "    return 1"; "  return n*fact(n-1)"; ""; "print(fact(7))"; ""; "def fib(n):"; "  if n <= 1:"; "    return n"; "  return fib(n-1) +"; "    fib(n-2)"; ""; "print(fib(7))"]
      SetFileType "python"
      Text ":file fact.py"; Enter
      Text ":w!"; Enter
      Text "ZZ"; Pause 1000; Text "clear"; Enter; Text "vi fact.py"; Enter
      Text ":set nohls"; Enter; Text ":"; Esc
      Restart
      Start "Reverting"
      SayWhile ("If we make a bunch of changes to this file...", Compound (150, [
        Key ("/", "search", "/")
        Text "fact"
        Enter
        Change Word
        Text "factorial"
        Esc
        Pause 500
        Move SearchNext
        Dot
        Pause 500
        Move SearchNext
        Dot
        Pause 500
        Move Down
        ZoomTop
        Pause 500
        Delete BottomOfDocument
        Pause 1000
        Move TopOfDocument
        ]))
      Say "And we change our mind and want to revert everything... we could either press U a bunch of times..."
      SayWhile ("...or choose the nuclear option with colon E bang.", Text ":e!")
      Enter
      SayWhile ("And boom! We've reverted to the last saved version.", Compound (100, [Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Pause 500; Move TopOfDocument]))
      Finish ]

let scrolling = // ^e ^y zt zb zz ^f ^b ^d ^u
    [ Launch
      Pause 5000
      Setup ["# Grocery List"; ""; "## Fruits"; ""; "- Apples"; "- Bananas"; "- Citrus"; "  - Oranges"; "  - Lemons"; "- Berries"; "  - Strawberries"; "  - Blueberries"; ""; "## Vegetables"; ""; "- Leafy Greens"; "  - Spinach"; "  - Kale"; "- Root Vegetables"; "  - Carrots"; "  - Potatoes"; ""; "### Dairy"; ""; "- Milk"; "- Cheese"; "- Yogurt"; ""; "### Bakery"; ""; "- Bread"; "  - Wheat"; "  - Rye"; "- Pastries"; ""; "## Meat"; ""; "- Chicken"; "- Beef"; "- Pork"]
      SetFileType "markdown"
      Move Down; Move Down; Move Down; Move Down; Move Down; Move Down; Move Down
      Restart
      Start "Scrolling and Jumping"
      Say "Sometimes we want to scroll the viewport without moving the cursor, and most importantly without reaching for the track pad or scroll wheel."
      SayWhile ("Control Y will scroll down, and control E will scroll up. Notice that the cursor stays in place within the document.", Compound (210, [ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollUp; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown; ScrollDown]))
      Pause 500
      Say "We can scroll to the top with Z T."
      ScrollTop
      Pause 1000
      Say "Or to the bottom with Z B."
      ScrollBottom
      Say "I often find it handy to scroll the cursor to the middle with Z Z."
      ScrollMiddle
      Pause 1000
      Say "If maintaining the cursor position isn't important..."
      SayWhile ("....then we can also jump forward by whole screens with control F", Compound (500, [JumpDown; JumpDown; JumpDown; JumpDown; JumpDown]))
      Pause 800
      SayWhile ("And jump backward with control B", Compound (500, [JumpUp; JumpUp; JumpUp; JumpUp; JumpUp]))
      Say "I find this disorienting though."
      SayWhile ("Instead I prefer to jump by half screens with control D do go down...", Compound (600, [JumpDownHalf; JumpDownHalf; JumpDownHalf; JumpDownHalf; JumpDownHalf; JumpDownHalf; JumpDownHalf; JumpDownHalf]))
      SayWhile ("...and control U to go up.", Compound (600, [JumpUpHalf; JumpUpHalf; JumpUpHalf; JumpUpHalf; JumpUpHalf; JumpUpHalf; JumpUpHalf; JumpUpHalf]))
      Finish ]

let changingCase = // ~ g~ gu gU  (combined with visual or motion)
   [ Launch
     Pause 5000
     Setup ["let add x y ="; "    x + y"; "let plus7 ="; "    add 7"; ""; "(* currying test *)"; "35 |> plus7 |>"; "printf \"Life: %i\""]
     SetFileType "ocaml"
     Move Paragraph; Move Down; Move Word
     Start "Changing Case"
     Say "We can quickly toggle the case of a character while in Normal mode, by pressing tilde."
     ToggleCase
     Say "Notice that it automatically advances the cursor."
     SayWhile ("So, we can press tilde several times and toggle the whole word.", Compound (300, [ToggleCase; ToggleCase; ToggleCase; ToggleCase; ToggleCase; ToggleCase; ToggleCase]))
     SayWhile ("But a better way...", Compound (180, [Undo; Undo; Undo; Undo; Undo; Undo; Undo; Undo]))
     Say "... is to use G followed by tilde, followed by a motion."
     SayWhile ("Such as G tilde W to toggle the word.", ToggelCaseMotion Word)
     Pause 1000
     Undo
     SayWhile ("Or another approach may be to select the word first with V E..", Compound (300, [Visual; Move WordEnd]))
     SayWhile ("...and then toggle the case with tilde.", ToggleCase)
     Say "It's the same number of key strokes."
     Pause 800
     Undo
     SayWhile ("One issue we might have is when the word is mixed case to begin with.", Compound (300, [ToggleCase; Move Left]))
     SayWhile ("Oops, toggling the case leaves the C lowercase.", ToggelCaseMotion Word)
     Undo
     SayWhile ("Instead of toggling, we can select the word again...", Compound (300, [Reselect]))
     SayWhile ("...and use G follow by Shift U for uppercase.", UpperCase)
     Pause 800
     Reselect
     SayWhile ("Or G followed by lowercase U to make it lowercase.", LowerCase)
     Finish ]

//  Basic Motions 1  h j k l ␣ ⌫
//  Basic Motions 2  w b e ge
//  Basic Motions 3  W B E gE
//  Basic Motions 4  $ ^ 0
//  Basic Motions 5  H L M gg G
//  Basic Motions 6  ) ( } {
//  Basic Motions 7  ]] [[ ][ []
//  Basic Motions 8  + -
//  Matching  %
//  Underscore  _  (Just like carrot except with count)
//  Find  f F t T ; ,
//  Search  * # n N / ?
//  Join  J gJ
//  Quitting   :q :q! ZQ  :wq :x ZZ  ^z  fg
//  Reverting  :e!
//  Scrolling  ^e ^y zt zb zz  ^d ^u ^f ^b
//  Case  ~ g~ gu gU  (combined with visual or motion)

//  Mark  m ' ` '' ``
//  Counts  :set nu  :set rnu  #j  #k  #w  #G  #H  #L  ...
//  Visual  v V ^v o gv '< '>  (bad habit possibly)
//  Undo  u U ^r
//  Dot  .
//  Insert  i a I A o O ⎋  (thick cursor, before/after)
//  Delete/Yank/Put  d dd D y yy Y p P
//  Change/Substitute  c cc C s S
//  Delete char/Replace  x X r R  (replace with <CR> to break lines -- removes trailing space)
//  Macros  q @ @@
//  Indenting  < <<  >>  :set
//  Commands  :
//  Registers  "
//  Formatting  = ==
//  Leader  \
//  Search & Replace  :s/foo/bar & :%s/foo/bar  also n.n. trick
//  Advanced 1  !
//  Advanced 3  K
//  Advanced 4  Q
//  Line Wrap  :set nowrap  :set number  gh gj gk gl g$ g^ (display vs. real lines)
//  Pattern * cw foo <esc> n . n . n .
//  Text objects
//  Surround?
//  :noremap ^ _  :nmap _ i <esc>
//  Unexpected motions: f{char} /foo
//  Vertical inserts (includding ragged edge)
//  Interacting with the shell:  :w !{cmd}  :r !{cmd}  !  !!  ^z