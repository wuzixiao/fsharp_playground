
namespace AlgorithmPuzzles

module puzzle2 =
    let parseInput (filePath: string) =
        // Read all lines from the file
        let lines_str = System.IO.File.ReadAllLines(filePath)
        
        // Parse each line into 5 integers
        let lines_lst = 
            lines_str 
            |> Array.map (fun line -> line.Split(' ') |> Array.map int |> Array.toList)
            |> Array.toList
        
        lines_lst

    let is_safe2 (line: int list) =
        match line with
        | [] | [_] -> true // Empty or single-element lists are considered "safe"
        | _ ->
            let isIncreasing = List.pairwise line |> List.forall (fun (x, y) -> x < y && y - x < 4)
            let isDecreasing = List.pairwise line |> List.forall (fun (x, y) -> x > y && x - y < 4)
            isIncreasing || isDecreasing
    let filteri (predicate: int -> 'T -> bool) (list: 'T list) : 'T list =
        list
        |> List.mapi (fun i x -> (i, x)) // Map each element to a tuple (index, value)
        |> List.filter (fun (i, x) -> predicate i x) // Filter based on the predicate
        |> List.map snd // Extract only the values

    let is_safe3 (line: int list) =
        // Helper to check if a list is strictly increasing
        let isIncreasing lst = 
            List.pairwise lst |> List.forall (fun (x, y) -> x < y && y - x < 4)

        // Helper to check if a list is strictly decreasing
        let isDecreasing lst = 
            List.pairwise lst |> List.forall (fun (x, y) -> x > y && x - y < 4)

        // Main logic
        match line with
        | [] | [_] -> true // Empty or single-element lists are safe
        | _ ->
            if isIncreasing line || isDecreasing line then
                true // Already safe
            else
                // Check if removing at most one element makes it safe
                let checkWithRemoval = 
                    line
                    |> List.mapi (fun i _ -> 
                        line |> filteri (fun j _ -> j <> i) // Remove one element
                    )
                    |> List.exists (fun lst -> isIncreasing lst || isDecreasing lst)
                checkWithRemoval

    let is_safe (line: (int*int*int*int*int)) = 
        let (a, b, c, d, e) = line
        printfn "%d, %d, %d, %d, %d" a b c d e
        printfn "%b"  true
        (a < b && b < c && c < d && d < e) || (a > b && b > c && c > d && d > e)

    let result (line_arr: (int*int*int*int*int) array) = 
        line_arr 
        |> Array.filter is_safe
        |> Array.length 
    let result2 (lines: int list list) = 
        let safe_lines = lines |> List.filter is_safe2
        List.length safe_lines
    let result3 (lines: int list list) = 
        let safe_lines = lines |> List.filter is_safe3
        List.length safe_lines

    let resolve =
        // let filePath = "inputs/day1.txt" 
        let filePath = "inputs/day2.txt" 

        let input = parseInput filePath

        printfn "%A" input

        result2 input

    let resolve_part2 =
        // let filePath = "inputs/day1.txt" 
        let filePath = "inputs/test_day2.txt" 

        let input = parseInput filePath

        result3 input
        