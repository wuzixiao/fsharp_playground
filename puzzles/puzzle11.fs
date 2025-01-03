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
    
    
    let rec blinkNtimes3 (n:int) (s: string): int64  =
    // n: number of times to blink
    // s: string to blink
    // returns: the string length of the result of blinking `s` `n` times
        if n = 0 then
            1
        else
            match s with
            | "0" -> blinkNtimes3 (n - 1) "1"
            | _ when s.Length % 2 = 0 ->
                let firstHalf = s.Substring(0, s.Length / 2) |> System.Int64.Parse |> string
                let secondHalf = s.Substring(s.Length / 2) |> System.Int64.Parse |> string
                blinkNtimes3 (n - 1) firstHalf + blinkNtimes3 (n - 1) secondHalf
            | _ -> blinkNtimes3 (n - 1) (System.Int64.Parse(s) * 2024L |> string)

    let rec blinkNtimes3WithCache (n:int) (cache: Dictionary<int * string, int64>) (s: string): int64  =
    // n: number of times to blink
    // s: string to blink
    // cache: dictionary to store previously computed results. Key: (n, s), Value: result
    // returns: the string length of the result of blinking `s` `n` times
        if n = 0 then
            1
        elif cache.ContainsKey((n, s)) then
            cache.[(n, s)]
        else
            let result =
                match s with
                | "0" -> blinkNtimes3WithCache (n - 1) cache "1"
                | _ when s.Length % 2 = 0 ->
                    let firstHalf = s.Substring(0, s.Length / 2) |> System.Int64.Parse |> string
                    let secondHalf = s.Substring(s.Length / 2) |> System.Int64.Parse |> string
                    blinkNtimes3WithCache (n - 1) cache firstHalf + blinkNtimes3WithCache (n - 1) cache secondHalf
                | _ -> blinkNtimes3WithCache (n - 1) cache (System.Int64.Parse(s) * 2024L |> string)
            
            cache.[(n, s)] <- result
            result

    let blinkNtimes3List (n:int) (sl: string list) : int64 =
        let cache = Dictionary<int * string, int64>()
        sl |> List.sumBy (blinkNtimes3WithCache n cache)

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

    // Cache for storing results of blinking a single string `s`, `n` times
    // Key:   (n, s)
    // Value: list of resulting strings
    let cache = Dictionary<int * string, string list>()

    /// Recursively blink a single string `s`, `count` times, with caching.
    let rec blinkNtimesForString (count: int) (s: string) : string list =
        match cache.TryGetValue((count, s)) with
        | true, cached -> 
            // Use previously computed result
            cached
        | _ ->
            // Compute and store in cache
            let result =
                if count = 0 then
                    [ s ]
                else
                    blinkOnce s
                    |> List.collect (blinkNtimesForString (count - 1))
            cache.[(count, s)] <- result
            result


    let lazyParallelBlinkNtimesCache (n: int) (input: seq<string>) : seq<string> =
        // The main loop that applies blinking `n` times over the sequence
        // in a chunked + parallel + lazy fashion.
        let rec loop count (acc: seq<string>) : seq<string> =
            if count = 0 then
                // Base case: no more blinking
                acc
            else
                // 1) Chunk the sequence to avoid processing everything at once.
                // 2) For each chunk, convert to an array to allow parallel map.
                // 3) Within parallel map, apply `blinkNtimesForString count`.
                // 4) Flatten the array of arrays to a single seq.
                // 5) Cache the resulting seq so that multiple enumerations won't re-run it.
                acc
                |> Seq.chunkBySize 1000
                |> Seq.map (fun chunk ->
                    chunk
                    |> Array.ofSeq
                    |> Array.Parallel.map (fun s -> blinkNtimesForString count s |> List.toArray)
                    |> Array.concat
                    |> Seq.ofArray )
                |> Seq.concat
                |> Seq.cache  // Ensures we don't recalculate this entire pipeline
                |> loop (count - 1)
        
        loop n input
