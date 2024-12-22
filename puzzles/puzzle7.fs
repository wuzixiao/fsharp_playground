namespace AlgorithmPuzzles

open System

module puzzle7 =
    type Operation =
        | Add
        | Multiply
        | Concatenation

    type InputInfo = { result: int64; Operands: int64 List }

    type Equation =
        { result: int64
          Operation: Operation List
          Operands: int64 List }

    let parseInputInfo (line: string) =
        let parts = line.Split(':')
        let result = int64 (parts.[0])

        let operands = parts.[1].Trim().Split(' ') |> Array.map int64 |> List.ofArray

        { result = result; Operands = operands }

    let parseInput (filePath: string) : InputInfo List =
        // Read all lines from the file
        let lines_str = System.IO.File.ReadAllLines(filePath)
        lines_str |> Array.map parseInputInfo |> List.ofArray

    let generateOperations (n: int64) : Operation List List =
        // Generate a list of operations of length 2 ** n. There are only two possible operations: Add and Multiply.
        //  if n is 2. The list will be [[Add; Add]; [Add; Multiply]; [Multiply; Add]; [Multiply; Multiply]]
        // if n is 3. The list will be [[Add; Add; Add]; [Add; Add; Multiply]; [Add; Multiply; Add]; [Add; Multiply; Multiply]; [Multiply; Add; Add]; [Multiply; Add; Multiply]; [Multiply; Multiply; Add]; [Multiply; Multiply; Multiply]]
        let rec generate (n: int64) (acc: Operation List) =
            if n = 0L then
                [ acc ]
            else
                [ Add; Multiply ] |> List.collect (fun op -> generate (n - 1L) (op :: acc))

        generate n []

    let generateOperations2 (n: int64) : Operation List List =
        // Generate a list of operations of length 3 ** n. There are only three possible operations: Add and Multiply and Concatenation.
        //  if n is 2. The list will be [[Add; Add]; [Add; Concatenation]; [Add; Multiply]; [Concatenation; Add]; [Concatenation; Concatenation]; [Concatenation; Multiply]; [Multiply; Add]; [Multiply; Concatenation]; [Multiply; Multiply]]
        // if n is 3. The list will be [[Add; Add; Add]; [Add; Add; Concatenation]; [Add; Add; Multiply]; [Add; Concatenation; Add]; [Add; Concatenation; Concatenation]; [Add; Concatenation; Multiply]; [Add; Multiply; Add]; [Add; Multiply; Concatenation]; [Add; Multiply; Multiply]; [Concatenation; Add; Add]; [Concatenation; Add; Concatenation]; [Concatenation; Add; Multiply]; [Concatenation; Concatenation; Add]; [Concatenation; Concatenation; Concatenation]; [Concatenation; Concatenation; Multiply]; [Concatenation; Multiply; Add]; [Concatenation; Multiply; Concatenation]; [Concatenation; Multiply; Multiply]; [Multiply; Add; Add]; [Multiply; Add; Concatenation]; [Multiply; Add; Multiply]; [Multiply; Concatenation; Add]; [Multiply; Concatenation; Concatenation]; [Multiply; Concatenation; Multiply]; [Multiply; Multiply; Add]; [Multiply; Multiply; Concatenation]; [Multiply; Multiply; Multiply]]$
        let rec generate (n: int64) (acc: Operation List) =
            if n = 0L then
                [ acc ]
            else
                [ Add; Multiply; Concatenation ]
                |> List.collect (fun op -> generate (n - 1L) (op :: acc))

        generate n []

    let generateEquations (input: InputInfo) : Equation List =
        let operations = generateOperations (int64 (input.Operands.Length) - 1L)

        operations
        |> List.map (fun ops ->
            { result = input.result
              Operation = ops
              Operands = input.Operands })

    let generateEquations2 (input: InputInfo) : Equation List =
        let operations = generateOperations2 (int64 (input.Operands.Length) - 1L)

        operations
        |> List.map (fun ops ->
            { result = input.result
              Operation = ops
              Operands = input.Operands })

    let solveEquation (equation: Equation) =
        let rec solve (result: int64) (operands: int64 List) (ops: Operation List) =
            match ops with
            | [] -> result
            | Add :: tail -> solve (result + operands.[0]) (List.tail operands) tail
            | Multiply :: tail -> solve (result * operands.[0]) (List.tail operands) tail
            | Concatenation :: tail -> solve (result * 10L + operands.[0]) (List.tail operands) tail

        solve equation.Operands.[0] equation.Operands.[1..] equation.Operation

    let validEquation (equation: Equation) =
        let rec solve (result: int64) (operands: int64 List) (ops: Operation List) =
            match ops with
            | [] -> result
            | Add :: tail -> solve (result + operands.[0]) (List.tail operands) tail
            | Multiply :: tail -> solve (result * operands.[0]) (List.tail operands) tail
            | Concatenation :: tail ->
                let factor = pown 10L (int (Math.Log10(float operands.[0]) + 1.0))
                solve (result * factor + operands.[0]) (List.tail operands) tail

        let ret = solve equation.Operands.[0] equation.Operands.[1..] equation.Operation

        printfn "Equation: %A, Result: %d" equation ret
        ret = equation.result


    let validInputInfo (input: InputInfo) =
        let equations = generateEquations input

        equations |> List.exists validEquation

    let validInputInfo2 (input: InputInfo) =
        let equations = generateEquations2 input

        equations |> List.exists validEquation


    let solve (filePath: string) =
        let inputs = parseInput filePath

        inputs |> List.filter validInputInfo |> List.map (fun x -> x.result) |> List.sum

    let solve2 (filePath: string) =
        let inputs = parseInput filePath

        inputs
        |> List.filter (fun input ->
            let equations = generateEquations2 input
            printfn "input: %A" input
            let isValid = equations |> List.exists validEquation
            printfn "Is valid: %b" isValid
            isValid)
        |> List.map (fun x -> x.result)
        |> List.sum
