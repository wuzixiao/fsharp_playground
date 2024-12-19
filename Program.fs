// Learn more about F# at http://fsharp.org

open System
open AlgorithmPuzzles


[<EntryPoint>]
let main argv =
    let filePath = "./inputs/day5.txt"
    let result = puzzle5.resolve_part2 filePath
    printfn "Result: %d" result

    0 // return an integer exit code
