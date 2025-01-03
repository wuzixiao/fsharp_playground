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
    let num = "125"
    // let result = blinkNtimes2(6, num)
    let result = blinkNtimes3 6 num

    Assert.Equal(7L, result)
    // Assert.Equal("2097446912", result.[0])
    // Assert.Equal("2", result.[3])
    // Assert.Equal("2", result.[-1])

[<Fact>]
let ``Test blinkNtime3List`` () =
    let nums = ["125";"17"]
    // let result = blinkNtimes2(6, num)
    let result = blinkNtimes3List 6 nums

    Assert.Equal(22L, result)


[<Fact>]
let ``Test blinkNtime3List With Full Input`` () =
    let nums = parseInput "../../../../inputs/day11.txt"
    let result = blinkNtimes3List 75 nums

    Assert.Equal(238317474993392L, result)