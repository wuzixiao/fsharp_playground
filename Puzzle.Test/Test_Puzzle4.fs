module Test_Puzzle4

open System
open Xunit
open AlgorithmPuzzles.puzzle4


[<Fact>]
let ``Test diagnal_matrix`` () =
    let matrix =
        [ [ '1'; '2'; '3'; '4'; '5' ]
          [ '1'; '2'; '3'; '4'; '5' ]
          [ 'a'; 'b'; 'c'; 'd'; 'e' ] ]

    let ret = extractDiagonals matrix

    Assert.True(true)

[<Fact>]
let ``Test extract 3*3 matrix`` () =
    let matrix =
        [ [ '1'; '2'; '3'; '4'; '5' ]
          [ '1'; '2'; '3'; '4'; '5' ]
          [ 'a'; 'b'; 'c'; 'd'; 'e' ] ]

    let xx = extractThreeByThree matrix 0 2

    Assert.True(true)

[<Fact>]
let ``Test find MAS patterns`` () =
    let matrix =
        [ [ 'S'; '2'; 'M'; '4'; 'S' ]
          [ '1'; 'A'; '3'; 'A'; '5' ]
          [ 'S'; 'b'; 'M'; 'd'; 'S' ] ]

    let patterns = findXMASPatterns matrix

    Assert.Equal(patterns.Length, 2)

[<Fact>]
let ``Test IsContained in 3*3`` () =
    let matrix = [ [ 'M'; '2'; 'S' ]; [ '1'; 'A'; '3' ]; [ 'M'; 'b'; 'S' ] ]

    let ret = isContainedInThreeByThree matrix

    Assert.True(ret)

[<Fact>]
let ``Test extract 3*3`` () =
    let expected_matrix = [ [ 'M'; '2'; 'S' ]; [ '1'; 'A'; '3' ]; [ 'M'; 'b'; 'S' ] ]

    let input = parseInput "../../../../inputs/test_day4.txt"

    let threeByThree = extractThreeByThree input 0 1


    Assert.Equal(expected_matrix.[0].[0], threeByThree.[0].[0])

[<Fact>]
let ``Test isContain`` () =
    let line = [ 'a'; 'b'; 'c' ]

    Assert.True(isContain "ab" line)
    Assert.True(isContain "ba" line)
    Assert.False(isContain "ab c" line)
    Assert.False(isContain "abb" line)
