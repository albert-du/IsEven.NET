module Tests

open System
open Xunit
open IsEven

[<Fact>]
let ``Even numbers should be even`` () =
    let r = isEven 2
    Assert.True r.Even

[<Fact>]
let ``Non-even numbers should not be even`` () =
    let r = isEven 1
    Assert.False r.Even

[<Fact>]
let ``Negative numbers should throw an error`` () =
    Assert.Throws<ArgumentException> (fun () -> isEven -1 |> ignore)

[<Fact>]
let ``Large numbers should throw an error`` () =
    Assert.Throws<ArgumentException> (fun () -> isEven 1_000_000 |> ignore)