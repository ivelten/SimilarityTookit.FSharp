module EvaluatorsTests

open System
open FsCheck.Xunit
open SimilarityToolkit.Evaluators
open Assertions
open Xunit

[<Fact>]
let ``ByteSimilarityEvaluator: should have correct evaluated type`` () =
    ByteSimilarityEvaluator().EvaluatedType |> equals typeof<byte>

[<Property>]
let ``ByteSimilarityEvaluator: distance equals absolute value of the difference`` (x : byte) (y : byte) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    ByteSimilarityEvaluator().EvaluateDistance(x, y) = expected