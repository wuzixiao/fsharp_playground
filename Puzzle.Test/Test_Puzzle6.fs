module Test_Puzzle6

open System
open Xunit
open AlgorithmPuzzles.puzzle6


[<Fact>]
let ``Test Parse Input`` () =
    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"

    Assert.Equal(map.Length, 10)
    Assert.Equal(map.[0].Length, 10)

[<Fact>]
let `` Test init guard`` () =
    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let ret = initGuard map

    Assert.Equal((6, 4), ret |> fst)
    Assert.Equal(Up, ret |> snd)


[<Fact>]
let ``Test can move`` () =
    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = ((6, 4), Up)
    Assert.True(canMove map g)

    let g = ((0, 4), Up)
    Assert.False(canMove map g)

    let g = ((3, 9), Right)
    Assert.False(canMove map g)

[<Fact>]
let ``Test next status`` () =
    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = ((6, 4), Up)
    let ret = nextStatus map g

    Assert.Equal(((5, 4), Up), snd (fst ret))

    let g = ((6, 4), Down)
    let ret = nextStatus map g

    Assert.Equal(((7, 4), Down), snd (fst ret))

    let g = ((6, 4), Right)
    let ret = nextStatus map g

    Assert.Equal(((6, 5), Right), snd (fst ret))

    let g = ((6, 4), Left)
    let ret = nextStatus map g

    Assert.Equal(((6, 3), Left), snd (fst ret))
    Assert.True(snd ret)

    let g = ((3, 1), Right)
    let ret = nextStatus map g

    Assert.Equal(((3, 1), Down), snd (fst ret))
    Assert.False(snd ret)

[<Fact>]
let ``Test move forward`` () =
    let g = ((6, 4), Up)
    let ret = moveForward g

    Assert.Equal(((5, 4), Up), ret)

    let g = ((6, 4), Down)
    let ret = moveForward g

    Assert.Equal(((7, 4), Down), ret)

    let g = ((6, 4), Right)
    let ret = moveForward g

    Assert.Equal(((6, 5), Right), ret)

    let g = ((6, 4), Left)
    let ret = moveForward g

    Assert.Equal(((6, 3), Left), ret)


[<Fact>]
let ``Test Move guard`` () =
    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = initGuard map
    let ret = moveGuard map g [| fst g |]

    let l = ret |> Set.ofArray |> Set.count

    Assert.Equal(41, l)
