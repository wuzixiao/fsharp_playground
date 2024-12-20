module Test_Puzzle5

open System
open Xunit
open AlgorithmPuzzles.puzzle5


[<Fact>]
let ``Test Parse Input`` () =
    let (orders, pages) = parseInput "../../../../inputs/test_day5.txt"

    Assert.Equal(orders.Length, 21)
    Assert.Equal(pages.Length, 6)

[<Fact>]
let ``Test invalid orders`` () =
    // let orders =Array.empty |> Array.append "10|20" |> Array.append "10|30"
    let orders = [| "10|20"; "10|30"; "20|30" |]
    let ret = invalidOrders orders

    Assert.Contains([| "30"; "10" |], ret)
    Assert.Equal(3, ret.Length)

[<Fact>]
let ``TEst generate page pair`` () =
    let input = "10,20,30,40"
    let ret = generatePagePair input
    Assert.Equal(6, ret.Length)

[<Fact>]
let ``Test resolve`` () =
    let filePath = "../../../../inputs/test_day5.txt"
    let ret = resolve filePath

    Assert.Equal(143, ret)

[<Fact>]
let ``Test resolve part2`` () =
    let filePath = "../../../../inputs/test_day5.txt"
    // let ret = resolve_part2 filePath
    let input = parseInput filePath
    let invalidOrders = invalidOrders (input |> fst)

    let ret =
        input
        |> snd
        |> Array.filter (fun line -> (generatePagePair line) |> hasCommonItems invalidOrders)
        |> Array.map (fun p -> fixPage invalidOrders (p.Split(',')))

    Assert.Equal(3, ret.Length)


[<Fact>]
let ``Test swap index`` () =
    let arr = [| "3"; "4"; "5" |]
    swapIndex arr (0, 2)

    Assert.Equal("3", arr.[2])
    Assert.Equal("5", arr.[0])

[<Fact>]
let ``Test invalid index`` () =
    let invalidOrders = [| [| "5"; "1" |]; [| "7"; "2" |]; [| "5"; "3" |] |]
    let page = [| "3"; "7"; "2"; "5"; "1" |]

    let ret = invalidIndex page invalidOrders

    Assert.Equal(2, ret.Length)

[<Fact>]
let ``Test fix page`` () =
    let invalidOrders = [| [| "5"; "1" |]; [| "7"; "2" |]; [| "5"; "3" |] |]

    let page = [| "3"; "7"; "2"; "5"; "1" |]

    let f = fixPage invalidOrders page

    Assert.Equal("7", f.[2])

[<Fact>]
let ``Test fix page 2`` () =
    let invalidOrders =
        [| [| "13"; "97" |]
           [| "47"; "97" |]
           [| "13"; "29" |]
           [| "29"; "75" |]
           [| "13"; "47" |] |]

    let page = [| "97"; "13"; "75"; "29"; "47" |]

    let f = fixPage2 invalidOrders page

    Assert.Equal("75", page.[2])
