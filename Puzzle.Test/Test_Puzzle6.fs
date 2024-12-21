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
    let ret = moveGuard map g [| g |]

    let l = ret |> Array.map fst |> Set.ofArray |> Set.count

    Assert.Equal(41, l)

[<Fact>]
let ``Test new step`` () =
    let o = (3, 4)
    let s = ((3, 3), Right)
    let ret = newStep o s

    Assert.Equal(((4, 3), Down), ret)

[<Fact>]
let ``Test new step2`` () =
    let o = (3, 5)
    let s = ((3, 3), Right)
    let ret = newStep o s

    Assert.Equal(((-100, -100), Right), ret)

[<Fact>]
let ``Test is working obstacle`` () =
    let o = (6, 3)

    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = initGuard map
    let steps = moveGuard map g [| g |]

    let ret = isWorkingObstable o steps map g

    Assert.True(ret)

[<Fact>]
let ``Test is working obstacle2`` () =
    let o = (7, 6)

    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = initGuard map
    let steps = moveGuard map g [| g |]

    let ret = isWorkingObstable o steps map g

    Assert.True(ret)

[<Fact>]
let ``Test is working obstacle4`` () =
    let o = (3, 4)

    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = initGuard map
    let steps = moveGuard map g [| g |]

    let ret = isWorkingObstable o steps map g

    Assert.False(ret)

[<Fact>]
let ``Test is working obstacle3`` () =
    let o = (7, 5)

    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = initGuard map
    let steps = moveGuard map g [| g |]

    let ret = isWorkingObstable o steps map g

    Assert.False(ret)


[<Fact>]
let ``Test Move guard part2`` () =
    let map: lab_map = parseInput "../../../../inputs/test_day6.txt"
    let g = initGuard map
    let steps = moveGuard map g [| g |]

    let r =
        steps
        |> Array.filter (fun s -> isWorkingObstable (fst s) steps map g)
        |> Array.map (fun s -> fst s)
        |> Array.distinct
        |> Array.length

    Assert.Equal(6, r)
