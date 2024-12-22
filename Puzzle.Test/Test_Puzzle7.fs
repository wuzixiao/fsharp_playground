module Test_Puzzle7

open System
open Xunit
open AlgorithmPuzzles.puzzle7


// [<Fact>]
// let ``Test Parse Input`` () =
//     let lines = parseInput "../../../../inputs/test_day7.txt"

//     Assert.Equal(lines.Length, 9)
//     //190: 10 19
//     Assert.Equal(190, lines.[0].result)
//     Assert.Equal(2, lines.[0].Operands.Length)
//     Assert.Equal(10, lines.[0].Operands.[0])

// [<Fact>]
// let ``Test Generate Operations 0`` () =
//     let ops = generateOperations 1

//     Assert.Equal(2, ops.Length)
//     Assert.True(ops |> List.exists (fun x -> x = [ Add ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply ]))

// [<Fact>]
// let ``Test Generate Operations`` () =
//     let ops = generateOperations 2

//     Assert.Equal(4, ops.Length)
//     Assert.True(ops |> List.exists (fun x -> x = [ Add; Add ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Add; Multiply ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Multiply ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Add ]))

// [<Fact>]
// let ``Test Generate Operations 2`` () =
//     let ops = generateOperations 3

//     Assert.Equal(8, ops.Length)
//     Assert.True(ops |> List.exists (fun x -> x = [ Add; Add; Add ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Add; Multiply; Add ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Add; Multiply ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Add; Add ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Multiply; Multiply ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Multiply; Add ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Add; Multiply; Multiply ]))
//     Assert.True(ops |> List.exists (fun x -> x = [ Add; Add; Multiply ]))

// [<Fact>]
// let ``Test Generate Equations`` () =
//     let input = { result = 190; Operands = [ 10; 19 ] }

//     let equations = generateEquations input

//     Assert.Equal(2, equations.Length)

//     Assert.True(
//         equations
//         |> List.exists (fun x -> x.result = 190 && x.Operation = [ Add ] && x.Operands = [ 10; 19 ])
//     )

//     Assert.True(
//         equations
//         |> List.exists (fun x -> x.result = 190 && x.Operation = [ Multiply ] && x.Operands = [ 10; 19 ])
//     )

//     Assert.False(
//         equations
//         |> List.exists (fun x -> x.result = 190 && x.Operation = [ Add; Add ] && x.Operands = [ 10; 19 ])
//     )

//     Assert.True(
//         equations
//         |> List.exists (fun x -> x.result = 190 && x.Operation = [ Multiply ] && x.Operands = [ 10; 19 ])
//     )

// [<Fact>]
// let ``Test Solve Equation`` () =
//     let equation =
//         { result = 190
//           Operation = [ Add ]
//           Operands = [ 10; 19 ] }

//     let result = solveEquation equation

//     Assert.Equal(29, result)

//     let equation =
//         { result = 190
//           Operation = [ Multiply ]
//           Operands = [ 10; 19 ] }

//     let result = solveEquation equation

//     Assert.Equal(190, result)

//     let equation =
//         { result = 190
//           Operation = [ Add; Add ]
//           Operands = [ 10; 19; 10 ] }

//     let result = solveEquation equation

//     Assert.Equal(39, result)

//     let equation =
//         { result = 190
//           Operation = [ Multiply ]
//           Operands = [ 10; 19 ] }

//     let result = solveEquation equation

//     Assert.Equal(190, result)

// [<Fact>]
// let ``Test Valid Equation`` () =
//     let equation =
//         { result = 190
//           Operation = [ Add ]
//           Operands = [ 10; 19 ] }

//     let result = validEquation equation

//     Assert.False(result)

//     let equation =
//         { result = 29
//           Operation = [ Add ]
//           Operands = [ 10; 19 ] }

//     let result = validEquation equation

//     Assert.True(result)

//     let equation =
//         { result = 190
//           Operation = [ Multiply ]
//           Operands = [ 10; 19 ] }

//     let result = validEquation equation

//     Assert.True(result)

//     let equation =
//         { result = 190
//           Operation = [ Add; Add ]
//           Operands = [ 10; 19; 10 ] }

//     let result = validEquation equation

//     Assert.False(result)

//     let equation =
//         { result = 39
//           Operation = [ Add; Add ]
//           Operands = [ 10; 19; 10 ] }

//     let result = validEquation equation

//     Assert.True(result)

//     let equation =
//         { result = 190
//           Operation = [ Multiply ]
//           Operands = [ 10; 19 ] }

//     let result = validEquation equation

//     Assert.True(result)

// [<Fact>]
let ``Test Puzzle 7`` () =
    let ret = solve "../../../../inputs/test_day7.txt"

    Assert.Equal(3749L, ret)

    let ret2 = solve2 "../../../../inputs/test_day7.txt"
    Assert.Equal(11387L, ret2)

    let ret3 = solve2 "../../../../inputs/day7.txt"
    printfn "Result: %A" ret3


// [<Fact>]
let ``Test generateOperations2`` () =
    let ops = generateOperations2 1

    Assert.Equal(3, ops.Length)
    Assert.True(ops |> List.exists (fun x -> x = [ Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Concatenation ]))

    let ops = generateOperations2 2

    Assert.Equal(9, ops.Length)
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Concatenation; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Concatenation; Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Concatenation; Concatenation ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Concatenation ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Concatenation ]))

    let ops = generateOperations2 3

    Assert.Equal(27, ops.Length)
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Add; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Multiply; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Add; Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Add; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Multiply; Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Multiply; Multiply; Add ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Multiply; Multiply ]))
    Assert.True(ops |> List.exists (fun x -> x = [ Add; Add; Multiply ]))

    let ops = generateOperations2 4
    Assert.Equal(81, ops.Length)

// [<Fact>]
let ``Test validInputInfo2`` () =
    // 581225299324: 81 75 1 2 91 876 6 1 24
    let input =
        { result = 581225299324L
          Operands = [ 81; 75; 1; 2; 91; 876; 6; 1; 24 ] }

    let ret = validInputInfo2 input

    Assert.True(ret)

// [<Fact>]
let ``Test generateEquations2`` () =
    // 581225299324: 81 75 1 2 91 876 6 1 24
    let input =
        { result = 581225299324L
          Operands = [ 81; 75; 1; 2; 91; 876; 6; 1; 24 ] }

    let ret = generateEquations2 input

    Assert.True(ret |> List.exists (fun x -> x.Operands = [ 81; 75; 1; 2; 91; 876; 6; 1; 24 ]))

    Assert.True(
        ret
        |> List.exists (fun x ->
            x.Operation = [ Multiply; Add; Multiply; Multiply; Multiply; Multiply; Add; Concatenation ])
    )

[<Fact>]
let ``Test valid equation`` () =
    let equation =
        { result = 581225299324L
          Operation = [ Multiply; Add; Multiply; Multiply; Multiply; Multiply; Add; Concatenation ]
          Operands = [ 81; 75; 1; 2; 91; 876; 6; 1; 24 ] }

    let ret = validEquation equation

    Assert.True(ret)
