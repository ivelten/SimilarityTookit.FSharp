namespace SimilarityToolkit.Evaluators

open System
open SimilarityToolkit.Abstractions

type ByteSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<byte>()
    override __.EvaluateDistance(x : byte, y : byte) =
        Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type SByteSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<sbyte>()
    override __.EvaluateDistance(x : sbyte, y : sbyte) =
        Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type DateTimeSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<DateTime>()
    override __.EvaluateDistance(x : DateTime, y : DateTime) =
        if x.Date = x && y.Date = y
        then (x - y).TotalDays |> Convert.ToDecimal
        else (x - y).TotalSeconds |> Convert.ToDecimal

type DecimalSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<decimal>()
    override __.EvaluateDistance(x : decimal, y : decimal) =
        try
            x - y |> Math.Abs
        with
        | :? OverflowException -> Decimal.MaxValue