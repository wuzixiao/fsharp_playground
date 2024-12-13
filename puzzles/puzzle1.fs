namespace AlgorithmPuzzles

module puzzle1 =
    let parseInput (filePath: string) =
        // Read all lines from the file
        let lines = System.IO.File.ReadAllLines(filePath)
        
        // Parse each line into a pair of integers
        let pairs = 
            lines 
            |> Array.map (fun line -> 
                let parts = line.Split("   ") |> Array.map int
                (parts.[0], parts.[1]))
        
        // Split the pairs into two separate lists
        let list1, list2 = 
            pairs 
            |> Array.fold (fun (l1, l2) (x, y) -> (x :: l1, y :: l2)) ([], [])
        
        (List.rev list1, List.rev list2) // Reverse to maintain original order

    let calculate (list1: int list) (list2: int list) =
        // Sort both lists
        let sortedList1 = List.sort list1
        let sortedList2 = List.sort list2

        // Calculate the distance between corresponding elements and sum them
        List.zip sortedList1 sortedList2
        |> List.map (fun (x, y) -> abs (x - y))
        |> List.sum

    let solve_part1 =
        // let filePath = "inputs/day1.txt" 
        let filePath = "inputs/test_day1.txt" 
        let list1, list2 = parseInput filePath
        calculate list1 list2

    let calculateFrequencyList (list1: int list) (list2: int list) =
        // Count occurrences of each element in list1 within list2
        list1
        |> List.map (fun x -> List.filter ((=) x) list2 |> List.length)

    let calculateSumOfProducts (list1: int list) (list3: int list) =
        // Multiply each pair of elements from list1 and list3, then sum the results
        List.zip list1 list3
        |> List.map (fun (x, y) -> x * y)
        |> List.sum
    
    let solve_part2 = 
        // let filePath = "inputs/day1.txt" 
        let filePath = "inputs/day1.txt" 
        let list1, list2 = parseInput filePath
        let list3 = calculateFrequencyList list1 list2
        calculateSumOfProducts list1 list3
