namespace AlgorithmPuzzles

module puzzle5 =
    let parseInput (filePath: string) =
        // Read all lines from the file
        let lines_str = System.IO.File.ReadAllLines(filePath)

        let i = lines_str |> Array.findIndex (fun l -> l = "")
        // return a tuple of array (orders, pages)
        // lines_str |> Array.splitAt i
        (lines_str.[0 .. i - 1], lines_str.[i + 1 ..])

    let invalidOrders (orders: array<string>) =
        // orders |> Array.map (fun o -> o.Split('|') |> Array.map int |> Array.rev)
        orders |> Array.map (fun o -> o.Split('|') |> Array.rev)

    let generatePagePair (input: string) =
        let pages = input.Split(',')
        let len = pages.Length

        [ for i in 0 .. pages.Length - 2 do
              for j in i + 1 .. pages.Length - 1 do
                  yield [| pages.[i]; pages.[j] |] ]
        |> List.toArray

    let hasNoCommonItems (array1: string array array) (array2: string array array) : bool =
        not (array2 |> Array.exists (fun item -> Array.contains item array1))

    let hasCommonItems (array1: string array array) (array2: string array array) : bool =
        array2 |> Array.exists (fun item -> Array.contains item array1)

    let findCommonItems (array1: string array array) (array2: string array array) =
        let set1 = Set.ofArray array1
        let set2 = Set.ofArray array2
        Set.intersect set1 set2 |> Set.toArray

    let arrayMidItem (arr: string array) =
        let mid = (arr.Length - 1) / 2
        arr.[mid]

    let resolve (filePath: string) =
        let input = parseInput filePath
        let invalidOrders = invalidOrders (input |> fst)

        input
        |> snd
        |> Array.filter (fun l -> (generatePagePair l) |> hasNoCommonItems invalidOrders)
        |> Array.map (fun l -> l.Split(',') |> arrayMidItem)
        |> Array.map int
        |> Array.sum

    let invalidIndex (invalidPages: string array) (invalidOrders: string array array) =
        let len = invalidPages.Length

        [ for i in 0 .. len - 2 do
              for j in i + 1 .. len - 1 do
                  if invalidOrders |> Array.contains [| invalidPages.[i]; invalidPages.[j] |] then
                      yield [| i; j |] ]

    let swapIndex (arr: string array) (i, j) =
        let temp = arr.[i]
        arr.[i] <- arr.[j]
        arr.[j] <- temp

    let fixPage (invalidOrders: string array array) (invalidPages: string array) : string array =
        let idx = invalidIndex invalidPages invalidOrders

        idx |> List.iter (fun i -> swapIndex invalidPages (i.[0], i.[1]))

        invalidPages


    let fixPage2 (invalidOrders: string array array) (invalidPages: string array) =
        let len = invalidPages.Length

        [ for i in 0 .. len - 2 do
              for j in i + 1 .. len - 1 do
                  if invalidOrders |> Array.contains [| invalidPages.[i]; invalidPages.[j] |] then
                      swapIndex invalidPages (i, j) ]
        |> ignore

        invalidPages

    let resolve_part2 (filePath: string) =
        let input = parseInput filePath
        let invalidOrders = invalidOrders (input |> fst)

        input
        |> snd
        |> Array.filter (fun l -> (generatePagePair l) |> hasCommonItems invalidOrders)
        |> Array.map (fun p -> fixPage2 invalidOrders (p.Split(',')))
        |> Array.map arrayMidItem
        |> Array.map int
        |> Array.sum
