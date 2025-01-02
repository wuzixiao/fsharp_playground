namespace AlgorithmPuzzles

open System
open System.Collections.Generic

module puzzle11 =
    let parseInput (filePath: string) : string list = 
        let ret = System.IO.File.ReadAllText(filePath).Split(" ")
        ret |> Array.toList


    // let blinkOnce (string s) : string list =
    //     match s with
    //     | "0" -> ["1"]
    //     | s.Length % 2 = 0-> s.[0..(s.length / 2) - 1] @ s.[(s.length / 2)..]
    //     | _ -> [System.Int32.Parse(s) * 1024 |> string ]
    

    let blinkOnce (s: string) : string list =
        match s with
        | "0" -> ["1"]
        | _ when s.Length % 2 = 0 ->
            let firstHalf = s.Substring(0, s.Length / 2) |> System.Int64.Parse |> string
            let secondHalf = s.Substring(s.Length / 2) |> System.Int64.Parse |> string
            [firstHalf; secondHalf]
        | _ ->
            [System.Int64.Parse(s) * 2024L |> string]

    let blinkNtimes (n: int, s: string) : string list =
        let mutable ret = [s]
        for i in 1..n do
            ret <- ret |> List.collect blinkOnce
        ret

    let blinkNtimes2 (n: int, sl: string list) : string list =
        let rec loop count acc =
            if count = 0 then
                acc |> Seq.toList
            else
                loop (count - 1) 
                    (acc 
                    |> Seq.chunkBySize 100 // Divide work into chunks
                    |> Seq.map (fun chunk -> 
                        chunk 
                        |> Array.ofSeq 
                        |> Array.Parallel.map (fun s -> blinkOnce s |> List.toArray) // Ensure consistent type
                        |> Array.concat // Concatenate arrays
                        |> Seq.ofArray)
                    |> Seq.concat) // Flatten the sequence of sequences
        loop n (sl |> Seq.ofList)

    let blinkNtimesWithFullCache (n: int) (sl: string list) : string list =
        let cache = Dictionary<int * string, string list>()

        // Caching wrapper for n-times blinkOnce
        let rec blinkNtimesWithCache (n: int) (s: string) : string list =
            if cache.ContainsKey((n, s)) then
                cache.[(n, s)]
            else
                let result =
                    if n = 0 then
                        [s] // Base case: no operations left
                    else
                        blinkOnce s
                        |> List.collect (blinkNtimesWithCache (n - 1)) // Recursive call
                cache.[(n, s)] <- result
                result

        // Apply blinkNtimesWithCache to the entire list
        sl
        |> List.collect (blinkNtimesWithCache n)