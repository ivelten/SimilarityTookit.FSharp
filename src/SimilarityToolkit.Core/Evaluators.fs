namespace SimilarityToolkit.Evaluators

open System
open SimilarityToolkit.Abstractions

type ByteSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<byte>()
    override __.EvaluateDistance(x : byte, y : byte) =
        Math.Abs(Convert.ToDecimal(x) - Convert.ToDecimal(y))