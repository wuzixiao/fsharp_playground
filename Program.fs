// Learn more about F# at http://fsharp.org

open System
open AlgorithmPuzzles


[<EntryPoint>]
let main argv =
    let result = puzzle3.resolve_part2
    printfn "Result: %d" result

    0 // return an integer exit code
