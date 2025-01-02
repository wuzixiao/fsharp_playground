module Test_Puzzle11

open System
open Xunit
open AlgorithmPuzzles.puzzle11

[<Fact>]
let ``Test Parse Input`` () =
    let nums = parseInput "../../../../inputs/day11.txt"

    Assert.Equal(8, nums.Length)

[<Fact>]
let ``Test blinkOnce`` () =
    let num = "1234"
    let result = blinkOnce num

    Assert.Equal(2, result.Length)
    Assert.Equal("12", result.[0])
    Assert.Equal("34", result.[1])

[<Fact>]
let ``Test blinkOnce2`` () =
    let num = "999"
    let result = blinkOnce num

    Assert.Equal(1, result.Length)
    Assert.Equal("2021976", result.[0])

[<Fact>]
let ``Test blinkNtimes`` () =
    let num = "1"
    let result = blinkNtimes(3, num)

    Assert.Equal(4, result.Length)
    Assert.Equal("2", result.[0])
    Assert.Equal("0", result.[1])
    Assert.Equal("2", result.[2])
    Assert.Equal("4", result.[3])

[<Fact>]
let ``Test blinkNtimes2`` () =
    let num = "125"
    let result = blinkNtimes(2, num)

    Assert.Equal(2, result.Length)
    Assert.Equal("253", result.[0])
    Assert.Equal("0", result.[1])



[<Fact>]
let ``Test blinkNtime3`` () =
    let num = ["125"; "17"]
    // let result = blinkNtimes2(6, num)
    let result = blinkNtimesWithFullCache 6 num


    Assert.Equal(22, result.Length)
    // Assert.Equal("2097446912", result.[0])
    // Assert.Equal("2", result.[3])
    // Assert.Equal("2", result.[-1])

[<Fact>]
let ``Test blinkNtime4`` () =
    let nums = parseInput "../../../../inputs/day11.txt"
    let result = blinkNtimesWithFullCache 75 nums

    printfn "%A" result.Length
