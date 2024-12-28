namespace AlgorithmPuzzles

open System

module puzzle9 =
    type DiskMap = string
    type FileLayout = string array

    let parseInput (filePath: string) : DiskMap = System.IO.File.ReadAllText(filePath)

    let populateGapBlock (n: int) : string = String.replicate n "."

    let populateFileBlock (n: int, digit: int) : string = String.replicate n (string digit)

    let populateFileLayout (map: DiskMap) : FileLayout =

        map
        |> Seq.mapi (fun (i: int) (c: char) ->
            let n = System.Int32.Parse(string c)
            if i % 2 = 0 then
                populateFileBlock (n, i / 2)
            else
                populateGapBlock (n))
        |> Seq.toArray

    let processFile (layout: FileLayout) : FileLayout =
        let mutable i = 0
        let mutable j = layout.Length - 1

        while i < j do
            if layout.[i] = "." && layout.[j] <> "." then
                layout.[i] <- layout.[j]
                layout.[j] <- "."
                i <- i + 1
                j <- j - 1
            else if layout.[i] = "." && layout.[j] = "." then
                j <- j - 1
            else if layout.[i] <> "." && layout.[j] = "." then
                i <- i + 1
                j <- j - 1
            else
                i <- i + 1

        layout

    let calculateChecksum (layout: FileLayout)  =
        layout |> Seq.mapi (fun i c -> if c <> "." then int64 i * int64(System.Int32.Parse(c)) else 0) |> Seq.sum

