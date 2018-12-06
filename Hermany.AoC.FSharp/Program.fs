// Learn more about F# at http://fsharp.org

open System
open System.IO

let ReadInput filePath = File.ReadLines(filePath)

let ReadInputAsIntSeq filePath =
    ReadInput filePath
        |> Seq.map Int32.Parse

let Day1Part1 input =
    ReadInputAsIntSeq input
        |> Seq.sum
    
[<EntryPoint>]
let main argv =
    
    let pt1 = Day1Part1 @"C:\Users\PHermany\source\repos\aoc\Hermany.AoC.Files\Input\2018\2018-12-01.txt"
    printfn "%i" pt1

    let s = Console.ReadLine()
    0
