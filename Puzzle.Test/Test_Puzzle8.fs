module Test_Puzzle8

open System
open Xunit
open AlgorithmPuzzles.puzzle8

[<Fact>]
let ``Test Parse Input`` () =
    let map = parseInput "../../../../inputs/test_day8.txt"

    Assert.Equal(12, map.Length)
    Assert.Equal(12, map.[0].Length)
    Assert.Equal('.', map.[11].[11])

[<Fact>]
let ``Test Find Antenna Pair`` () =
    let map = parseInput "../../../../inputs/test_day8.txt"

    let pairs = findAntennaPair (ConvertMap map)

    Assert.Equal(9, pairs.Length)
    Assert.True(pairs |> Array.exists (fun x -> x = (('A', (5, 6)), ('A', (8, 8)))))


[<Fact>]
let ``Test populateAntinode`` () =
    let map = parseInput "../../../../inputs/test_day8.txt"

    let pairs = findAntennaPair (ConvertMap map)

    let antinodes = populateAntinode pairs.[0]

    Assert.Equal(2, antinodes.Length)
    Assert.True(antinodes |> Array.exists (fun x -> x = (3, 2)))
    Assert.True(antinodes |> Array.exists (fun x -> x = (0, 11)))


[<Fact>]
let ``Test Find Antinode`` () =
    let map = parseInput "../../../../inputs/test_day8.txt"

    let antinodes = findAntinode map

    Assert.Equal(14, antinodes.Length)

[<Fact>]
let ``Find Antinode`` () =
    let map = parseInput "../../../../inputs/day8.txt"

    let antinodes = findAntinode map

    Assert.Equal(295, antinodes.Length)


// [<Fact>]
let ``Find Antinode part2`` () =
    let map = parseInput "../../../../inputs/day8.txt"

    let antinodes = findAntinode2 map

    Assert.Equal(1034, antinodes.Length)

// [<Fact>]
let ``Test Populate Antinode 2`` () =
    let map = parseInput "../../../../inputs/test_day8.txt"

    let pairs = findAntennaPair (ConvertMap map)

    let antinodes = populateAntinode2 map pairs.[0]
    printfn "%A" antinodes

    Assert.Equal(4, antinodes.Length)
    Assert.True(antinodes |> Array.exists (fun x -> x = (3, 2)))
    Assert.True(antinodes |> Array.exists (fun x -> x = (0, 11)))
