module Test_Puzzle9

open System
open Xunit
open AlgorithmPuzzles.puzzle9

[<Fact>]
let ``Test Parse Input`` () =
    let map = parseInput "../../../../inputs/test_day9.txt"

    Assert.Equal(19, map.Length)

[<Fact>]
let ``Test Populate Gap Block`` () =
    let gapBlock = populateGapBlock 5

    Assert.Equal(".....", gapBlock)

[<Fact>]
let ``Test Populate File Block`` () =
    let fileBlock = populateFileBlock (15, 3)

    Assert.Equal("333333333333333", fileBlock)

[<Fact>]
let ``Test Populate File Layout`` () =
    let map = parseInput "../../../../inputs/test_day9.txt"
    let fileLayout = populateFileLayout map

    Assert.Equal("00...111...2...333.44.5555.6666.777.888899", fileLayout)
