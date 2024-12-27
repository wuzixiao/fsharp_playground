namespace AlgorithmPuzzles

open System

module puzzle9 =
    type DiskMap = string
    type FileLayout = string

    let parseInput (filePath: string) : string = System.IO.File.ReadAllText(filePath)

    let populateGapBlock (n: int) : string = String.replicate n "."

    let populateFileBlock (n: int, digit: int) : string = String.replicate n (string digit)

    let populateFileLayout (map: DiskMap) : FileLayout =

        map
        |> Seq.mapi (fun i c ->
            let n = int (System.Char.GetNumericValue(c))
            if i % 2 = 0 then
                populateFileBlock (n, i / 2)
            else
                populateGapBlock (n))

        |> String.concat ""

    let processFile (layoutS: FileLayout) : FileLayout =
        let layout = layoutS.ToCharArray()
        let mutable i = 0
        let mutable j = layout.Length - 1

        while i < j do
            if layout.[i] = '.' && layout.[j] <> '.' then
                layout.[i] <- layout.[j]
                layout.[j] <- '.'
                i <- i + 1
                j <- j - 1
            else if layout.[i] = '.' && layout.[j] = '.' then
                j <- j - 1
            else if layout.[i] <> '.' && layout.[j] = '.' then
                i <- i + 1
                j <- j - 1
            else
                i <- i + 1

        layout |> FileLayout

    let calculateChecksum (layout: FileLayout) : int =
        layout |> Seq.mapi (fun i c -> if c <> '.' then i * int c else 0) |> Seq.sum
