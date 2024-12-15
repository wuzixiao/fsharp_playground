module Tests

open System
open Xunit
open AlgorithmPuzzles.puzzle2
    
[<Fact>]
let ``Test is_safe2 with increasing list`` () =
    let result = is_safe2 [1; 2; 3; 4; 5]
    Assert.True(result)

[<Fact>]
let ``Test is_safe with increasing list`` () =
    let result = is_safe (1,2, 3, 4, 5)
    Assert.True(result)
    
[<Fact>]
let ``Test is_safe3 with increasing list`` () =
    let result = is_safe3 [1;2;3;4;5;0]
    Assert.True(result)

[<Fact>]
let ``Test result2 with a list of list int`` () =
    let testData : int list list = 
        [
            [1; 2; 3; 4; 5];  // A list of integers
            [5; 4; 3; 2; 1];  // Another list of integers
            [1; 1; 1; 1; 1];  // A constant list
            [1; 2; 1; 3; 4]   // A mixed-direction list
            [1; 2; 1; 3; 4]   // A mixed-direction list
            [1; 2; 1; 3; 4]   // A mixed-direction list
        ]

    let ret = result2 testData
    Assert.Equal(2, ret)


