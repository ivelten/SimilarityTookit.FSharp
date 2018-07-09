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
let ``Int16SimilarityEvaluator: distance equals absolute value of the difference`` (x : int16) (y : int16) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    Int16SimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``UInt16SimilarityEvaluator: distance equals absolute value of the difference`` (x : uint16) (y : uint16) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    UInt16SimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``UInt32SimilarityEvaluator: distance equals absolute value of the difference`` (x : uint32) (y : uint32) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    UInt32SimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``Int32SimilarityEvaluator: distance equals absolute value of the difference`` (x : int) (y : int) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    Int32SimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``Int64SimilarityEvaluator: distance equals absolute value of the difference`` (x : int64) (y : int64) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    Int64SimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``UInt64SimilarityEvaluator: distance equals absolute value of the difference`` (x : uint64) (y : uint64) =
    let expected = Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs
    UInt64SimilarityEvaluator().EvaluateDistance(x, y) = expected

[<Property>]
let ``SingleSimilarityEvaluator: distance equals absolute value of the difference`` (x : single) (y : single) =
    let evaluator = SingleSimilarityEvaluator()
    try
        let expected = x - y |> Convert.ToDecimal |> Math.Abs
        evaluator.EvaluateDistance(x, y) = expected
    with
    | :? OverflowException -> evaluator.EvaluateDistance(x, y) = Decimal.MaxValue

[<Property>]
let ``DoubleSimilarityEvaluator: distance equals absolute value of the difference`` (x : double) (y : double) =
    let evaluator = DoubleSimilarityEvaluator()
    try
        let expected = x - y |> Convert.ToDecimal |> Math.Abs
        evaluator.EvaluateDistance(x, y) = expected
    with
    | :? OverflowException -> evaluator.EvaluateDistance(x, y) = Decimal.MaxValue

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

[<Fact>]
let ``SingleSimilarityEvaluator: if difference causes an overflow, return decimal's max value`` () =
    SingleSimilarityEvaluator().EvaluateDistance(Single.MaxValue, -1.0f) |> equals Decimal.MaxValue

[<Fact>]
let ``DoubleSimilarityEvaluator: if difference causes an overflow, return decimal's max value`` () =
    DoubleSimilarityEvaluator().EvaluateDistance(Double.MaxValue, -1.0) |> equals Decimal.MaxValue

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