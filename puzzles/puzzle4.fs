namespace AlgorithmPuzzles

module puzzle4 =
    let parseInput (filePath: string) =
        // Read all lines from the file
        let lines_str = System.IO.File.ReadAllLines(filePath)

        // Parse each line into 5 integers
        let lines_lst =
            lines_str |> Array.map (fun line -> line |> List.ofSeq) |> Array.toList

        lines_lst

    let extractRows (matrix: char list list) : char list list = matrix

    let extractColumns (matrix: char list list) : char list list =
        let rows = List.length matrix
        let cols = List.length (List.head matrix)
        [ for c in 0 .. cols - 1 -> [ for r in 0 .. rows - 1 -> matrix.[r].[c] ] ]

    let extractDiagonals (matrix: char list list) : char list list =
        let rows = List.length matrix
        let cols = List.length (List.head matrix)

        // Top-right to bottom-left diagonals
        let primaryDiagonals =
            [ for d in 0 .. (rows + cols - 2) ->
                  [ for r in 0 .. rows - 1 do
                        let c = d - r

                        if c >= 0 && c < cols then
                            matrix.[r].[c] ] ]

        // Top-left to bottom-right diagonals
        let secondaryDiagonals =
            [ for d in 0 .. (rows + cols - 2) ->
                  [ for r in 0 .. rows - 1 do
                        let c = r - d + (cols - 1)

                        if c >= 0 && c < cols then
                            matrix.[r].[c] ] ]

        primaryDiagonals @ secondaryDiagonals

    let isContain (word: string) (line: char list) =
        System.String(line |> List.toArray).Contains word
        || System.String(line |> List.toArray |> Array.rev).Contains word


    let countWordInLine (line: char list) (word: string) : int =
        let wordChars = word |> List.ofSeq
        let wordLength = List.length wordChars
        let reversedWordChars = List.rev wordChars

        line
        |> List.windowed wordLength
        |> List.filter (fun window -> window = wordChars || window = reversedWordChars)
        |> List.length

    let extractThreeByThree (matrix: char list list) (row: int) (col: int) =
        [ [ matrix.[row].[col]; matrix.[row].[col + 1]; matrix.[row].[col + 2] ]

          [ matrix.[row + 1].[col]
            matrix.[row + 1].[col + 1]
            matrix.[row + 1].[col + 2] ]

          [ matrix.[row + 2].[col]
            matrix.[row + 2].[col + 1]
            matrix.[row + 2].[col + 2] ] ]

    let isContainedInThreeByThree (matrix: char list list) =
        isContain "MAS" [ matrix.[0].[0]; matrix.[1].[1]; matrix.[2].[2] ]
        && isContain "MAS" [ matrix.[0].[2]; matrix.[1].[1]; matrix.[2].[0] ]

    let findXMASPatterns (grid: char list list) : (int * int) list =
        [ for row in 0 .. List.length grid - 3 do
              for col in 0 .. List.length (List.head grid) - 3 do
                  let threeByThree = extractThreeByThree grid row col

                  if isContainedInThreeByThree threeByThree then
                      yield (row, col) ]

    let resolve =
        let filePath = "inputs/day4.txt"
        // let filePath = "inputs/test_day4.txt"
        let input = parseInput filePath
        let matrix = extractRows input @ extractColumns input @ extractDiagonals input
        let word = "XMAS"


        matrix |> List.sumBy (fun line -> countWordInLine line word)

    let resolve_part2 =
        let filePath = "inputs/day4.txt"
        // let filePath = "inputs/test_day4.txt"
        let input = parseInput filePath

        let points = findXMASPatterns input

        points.Length
