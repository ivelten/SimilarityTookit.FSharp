module EvaluatorsTests

open System
open FsCheck.Xunit
open SimilarityToolkit.Evaluators

[<Property>]
let ``ByteSimilarityEvaluator: distance equals absolute value of the difference`` (x : byte) (y : byte) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    ByteSimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``SByteSimilarityEvaluator: distance equals absolute value of the difference`` (x : sbyte) (y : sbyte) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    SByteSimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``DecimalSimilarityEvaluator: distance equals absolute value of the difference`` (x : decimal) (y : decimal) =
    let expected = x - y |> Math.Abs
    DecimalSimilarityEvaluator().EvaluateDistance(x, y) = expected