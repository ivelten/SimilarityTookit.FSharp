module EvaluatorsTests

open System
open FsCheck.Xunit
open SimilarityToolkit.Evaluators
open Assertions
open Xunit

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
    let evaluator = DecimalSimilarityEvaluator()
    try
        let expected = x - y |> Math.Abs
        evaluator.EvaluateDistance(x, y) = expected
    with
    | :? OverflowException -> evaluator.EvaluateDistance(x, y) = Decimal.MaxValue

[<Fact>]
let ``DecimalSimilarityEvaluator: if difference causes an overflow, return decimal's max value`` () =
    DecimalSimilarityEvaluator().EvaluateDistance(Decimal.MaxValue, -1M) |> equals Decimal.MaxValue

[<Property>]
let ``DateTimeSimilarityEvaluator: distance equals difference in days or seconds`` (x : DateTime) (y : DateTime) =
    let expected =
        if x.Date = x && y.Date = y
        then (x - y).TotalDays |> Convert.ToDecimal
        else (x - y).TotalSeconds |> Convert.ToDecimal
    DateTimeSimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Fact>]
let ``DateTimeSimilarityEvaluator: if both dates don't have a time, returns distance in days`` () =
    let x = DateTime(2000, 2, 10)
    let y = DateTime(2000, 2, 1)
    DateTimeSimilarityEvaluator().EvaluateDistance(x, y) |> equals 9M

[<Fact>]
let ``DateTimeSimilarityEvaluator: if one of dates have a time, return distance in seconds`` () =
    let x = DateTime(2000, 2, 10, 13, 20, 10)
    let y = DateTime(2000, 2, 10)
    DateTimeSimilarityEvaluator().EvaluateDistance(x, y) |> equals 48010M