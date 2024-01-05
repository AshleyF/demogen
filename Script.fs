﻿module Script

open System
open System.Threading
open System.Windows.Forms
open NeuralVoice // or SpeechSynth
//open SpeechSynth // or NeuralVoice

let both f0 f1 =
    let t0 = new Thread(new ThreadStart(f0))
    let t1 = new Thread(new ThreadStart(f1))
    t0.Start()
    t1.Start()
    t0.Join()
    t1.Join()

let pause ms = (new Random()).Next(50) + ms |> Thread.Sleep

let key k = SendKeys.SendWait(k)

let typing text =
    for k in text do
        string k |> key
        pause 45

type Motion =
    | Up
    | Down
    | Right
    | Left
    | Word
    | BigWord
    | BackWord
    | BackBigWord
    | WordEnd
    | BigWordEnd
    | BackWordEnd
    | BackBigWordEnd
    | EndOfLine
    | StartOfLine
    | FirstColumn
    | LowestLine
    | HighestLine
    | MiddleLine
    | TopOfDocument
    | BottomOfDocument
    | Sentence
    | BackSentence
    | Paragraph
    | BackParagraph
    | SectionStart
    | BackSectionStart
    | SectionEnd
    | BackSectionEnd

type Action =
    | Launch
    | Start of string
    | Finish
    | Setup of string seq
    | Esc
    | Text of string
    | Normal of string * string
    | Pause of int
    | Insert
    | InsertBefore
    | Enter
    | After
    | AfterLine
    | Repeat
    | Undo
    | OpenBelow
    | OpenAbove
    | ZoomMiddle
    | DeleteLine
    | YankToLastLine
    | Put
    | PutBefore
    | Record of char
    | StopRecording
    | Macro of char
    | RepeatLastMacro
    | RepeatMacro of char * int
    | SelectBlock
    | AroundBlock // text object
    | Reselect
    | Increment
    | IncrementOrdinals
    | Say of string
    | SayWhile of string * Action
    | Compound of int * Action seq
    | QuitWithoutSaving
    | SetFileType of string
    | Find of char
    | JoinLine
    | Key of string * string * string
    | Move of Motion

let shift c = if Char.IsUpper(c) then $"⇧{c}" else c.ToString()

let rec edit = function
    | Launch -> KeyCast.set "Starting" "One moment..."; key "^({ESC}E)"; pause 500; key "nvim"; pause 500; key "{ENTER}"; pause 5000; key ":file Fu{ENTER}:{ESC}"; pause 4000
    | Start message -> KeyCast.set "VimFu" message; pause 800
    | Finish -> pause 800; KeyCast.set "Finished" "Cut!"; key "{ESC}:q!{ENTER}"
    | Setup lines -> key ":set noautoindent{ENTER}{ESC}i"; lines |> Seq.iter (fun line -> key line; key "{ENTER}"); key "{ESC}ddgg0:set autoindent{ENTER}:{ESC}"
    | Esc -> KeyCast.set "⎋" "normal mode"; key "{ESC}"
    | Text text -> KeyCast.set "⌨" ""; typing text
    | Normal (text, caption) -> KeyCast.set text caption; typing text
    | Pause ms -> pause ms
    | Insert -> KeyCast.set "i" "insert"; key "i"
    | InsertBefore -> KeyCast.set "⇧I" "insert before"; key "I"
    | Enter -> KeyCast.set "⌨" ""; key "{ENTER}"
    | After -> KeyCast.set "a" "after"; key "A"
    | AfterLine -> KeyCast.set "⇧A" "after line"; key "A"
    | Move Up -> KeyCast.set "k" "up"; key "k"
    | Move Down -> KeyCast.set "j" "down"; key "j"
    | Move Right -> KeyCast.set "l" "right"; key "l"
    | Move Left -> KeyCast.set "h" "left"; key "h"
    | Move Word -> KeyCast.set "w" "forward word"; key "w"
    | Move BigWord -> KeyCast.set "⇧W" "forward WORD"; key "W"
    | Move BackWord -> KeyCast.set "b" "back word"; key "b"
    | Move BackBigWord -> KeyCast.set "⇧B" "back WORD"; key "B"
    | Move WordEnd -> KeyCast.set "e" "end of word"; key "e"
    | Move BigWordEnd -> KeyCast.set "⇧E" "end of WORD"; key "E"
    | Move BackWordEnd -> KeyCast.set "ge" "prev end of word"; key "ge"
    | Move BackBigWordEnd -> KeyCast.set "g⇧E" "prev end of WORD"; key "gE"
    | Move EndOfLine -> KeyCast.set "⇧$" "end of line"; key "$"
    | Move StartOfLine -> KeyCast.set "⇧^" "start of line"; key "{^}"
    | Move FirstColumn -> KeyCast.set "0" "column zero"; key "0"
    | Move LowestLine -> KeyCast.set "⇧L" "lowest line"; key "L"
    | Move HighestLine -> KeyCast.set "⇧H" "highest line"; key "H"
    | Move MiddleLine -> KeyCast.set "⇧M" "middle line"; key "M"
    | Move TopOfDocument -> KeyCast.set "gg" "go top"; key "gg"
    | Move BottomOfDocument -> KeyCast.set "⇧G" "go bottom"; key "G"
    | Move Sentence -> KeyCast.set "⇧)" "next sentence"; key "{)}"
    | Move BackSentence -> KeyCast.set "⇧(" "prev sentence"; key "{(}"
    | Move Paragraph -> KeyCast.set "⇧}" "next paragraph"; key "{}}"
    | Move BackParagraph -> KeyCast.set "⇧{" "prev paragraph"; key "{{}"
    | Move SectionStart -> KeyCast.set "]]" "next section start"; key "{]}{]}"
    | Move BackSectionStart -> KeyCast.set "[[" "prev section start"; key "{[}{[}"
    | Move SectionEnd -> KeyCast.set "][" "next section end"; key "{]}{[}"
    | Move BackSectionEnd -> KeyCast.set "[]" "prev section end"; key "{[}{]}"
    | Repeat -> KeyCast.set "." "repeat"; key ".";
    | Undo -> KeyCast.set "u" "undo"; key "u"
    | OpenBelow -> KeyCast.set "o" "open below"; key "o"
    | OpenAbove -> KeyCast.set "⇧O" "open above"; key "O"
    | ZoomMiddle -> KeyCast.set "zz" "zoom middle"; key "zz"
    | DeleteLine -> KeyCast.set "dd" "delete line"; key "dd"
    | YankToLastLine -> KeyCast.set "y⇧G" "yank to last line"; key "yG"
    | Put -> KeyCast.set "p" "put"; key "p"
    | PutBefore -> KeyCast.set "⇧P" "put before"; key "P"
    | Record register -> KeyCast.set $"q{shift register}" $"record into {register}"; key $"q{register}"
    | StopRecording  -> KeyCast.set $"q" "stop recording"; key "q"
    | Macro register -> KeyCast.set $"⇧@{shift register}" $"play macro {register}"; key $"@{register}"
    | RepeatLastMacro -> KeyCast.set "⇧@@" "repeat last macro"; key "@@"
    | RepeatMacro (register, n) -> KeyCast.set $"{n}⇧@{shift register}" $"repeat macro {register} {n} times"; key $"{n}@{register}"
    | SelectBlock -> KeyCast.set "⌃v" "select block"; key "^q" // CTRL-Q because CTRL-V is mapped to paste
    | AroundBlock -> KeyCast.set "a⇧B" "around block"; key "aB"
    | Reselect -> KeyCast.set "gv" "reselect visual"; key "gv"
    | Increment -> KeyCast.set "⌃A" "increment"; key "^a"
    | IncrementOrdinals -> KeyCast.set "g⌃a" "increment ordinals"; key "g^a"
    | Say text -> say text
    | SayWhile (text, action) -> both (fun () -> say text) (fun () -> edit action)
    | Compound (wait, actions) -> Seq.iter (fun a -> pause wait; edit a) actions
    | QuitWithoutSaving -> KeyCast.set ":q!⏎" "quit without saving"; key ":q!{ENTER}"
    | SetFileType kind -> key $":set filetype={kind}"; key "{ENTER}:{ESC}"; pause 2000
    | Find c -> KeyCast.set $"f{shift c}" $"find '{c}'"; key $"f{c}"
    | JoinLine -> KeyCast.set "⇧J" "join line"; key "J"
    | Key (cast, desc, k) -> KeyCast.set cast desc; key k


let rec go = function
    | action :: tail -> edit action; go tail
    | [] -> ()