
namespace AlgorithmPuzzles

open System.Text.RegularExpressions

module puzzle3 =
    let parseInput_as_string (filePath: string) =
        // Read all lines from the file
        let lines_str = System.IO.File.ReadAllLines(filePath)
        
        String.concat "" lines_str

    let findMulPatterns (input: string) =
        let pattern = @"mul\((\d{1,3}),(\d{1,3})\)"
        let regex = Regex(pattern)
        let matches = regex.Matches(input)
        
        let ret = 
            [ for m in matches ->
                let x = int (m.Groups.[1].Value) // First number (1-3 digits)
                let y = int (m.Groups.[2].Value) // Second number (1-3 digits)
                (m.Value, x, y) // Return the full match and the numbers
            ]

        ret

    let calculate_result_part2 (input:string) =
    // Regex patterns for all instructions
        let mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)"
        let doPattern = @"do\(\)"
        let dontPattern = @"don't\(\)"
        
        // Regex to find any matching instruction
        let allPattern = $@"{mulPattern}|{doPattern}|{dontPattern}"
        let matches = Regex.Matches(input, allPattern)
        
        // Initial state
        let mutable isEnabled = true
        let results = System.Collections.Generic.List<int>()

        // Process each match
        for m in matches do
            let matchStr = m.Value
            if Regex.IsMatch(matchStr, doPattern) then
                isEnabled <- true
            elif Regex.IsMatch(matchStr, dontPattern) then
                isEnabled <- false
            elif Regex.IsMatch(matchStr, mulPattern) then
                if isEnabled then
                    let mulMatch = Regex.Match(matchStr, mulPattern)
                    let x = int mulMatch.Groups.[1].Value
                    let y = int mulMatch.Groups.[2].Value
                    results.Add(x * y)

        results |> Seq.sum

    let resolve =
        let filePath = "inputs/day3.txt" 

        let input = parseInput_as_string filePath

        findMulPatterns input
        |> List.map (fun (_,x,y) -> x*y)
        |> List.sum

    let resolve_part2 =
        let filePath = "inputs/day3.txt" 

        let input = parseInput_as_string filePath

        calculate_result_part2 input
