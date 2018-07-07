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

type DecimalSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<decimal>()
    override __.EvaluateDistance(x : decimal, y : decimal) =
        x - y |> Math.Abs