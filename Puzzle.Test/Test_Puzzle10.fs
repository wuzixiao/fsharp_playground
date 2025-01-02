module Test_Puzzle10

open System
open Xunit
open AlgorithmPuzzles.puzzle10

[<Fact>]
let ``Test Parse Input`` () =
    let map = loadMap "../../../../inputs/test_day10.txt"

    Assert.Equal(8, map.Length)
    Assert.Equal(8, map.[0].Length)
    Assert.Equal(8, map.[0].[0].value)

[<Fact>]
let ``Test find all trail head`` () =
    let map = loadMap "../../../../inputs/test_day10.txt"
    let trailHeads = findTrailHead map

    Assert.Equal(9, trailHeads.Length)
    Assert.Contains({ x = 0; y = 2; value = 0 }, trailHeads)

[<Fact>]
let ``Test Valid Next Point`` () =
    let map = loadMap "../../../../inputs/test_day10.txt"
    let valid = validNextPoint(map, 0, 0, 7)

    Assert.True(valid)

[<Fact>]
let ``Test Trail Head Score`` () =
    let map = loadMap "../../../../inputs/test_day10.txt"
    let score = trailHeadScore(map, { x = 0; y = 2; value = 0 })

    Assert.Equal(5, score)

[<Fact>]
let ``Test sumAllTrailHeadScore`` () =
    // let map = loadMap "../../../../inputs/test_day10.txt"
    let map = loadMap "../../../../inputs/day10.txt"
    let score = sumAllTrailHeadScore map

    Assert.Equal(682, score)

[<Fact>]
let ``Test sumAllTrailHeadRating`` () =
    // let map = loadMap "../../../../inputs/test_day10.txt"
    let map = loadMap "../../../../inputs/day10.txt"
    let score = sumAllTrailHeadRating map

    Assert.Equal(1511, score)