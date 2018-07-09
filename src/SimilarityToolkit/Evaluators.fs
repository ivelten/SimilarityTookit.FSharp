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

type Int16SimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<int16>()
    override __.EvaluateDistance(x : int16, y : int16) =
        Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type UInt16SimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<uint16>()
        override __.EvaluateDistance(x : uint16, y : uint16) =
            Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type UInt32SimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<uint32>()
        override __.EvaluateDistance(x : uint32, y : uint32) =
            Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type Int32SimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<int>()
    override __.EvaluateDistance(x : int, y : int) =
        Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type Int64SimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<int64>()
        override __.EvaluateDistance(x : int64, y : int64) =
            Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type UInt64SimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<uint64>()
        override __.EvaluateDistance(x : uint64, y : uint64) =
            Convert.ToDecimal(x) - Convert.ToDecimal(y) |> Math.Abs

type SingleSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<single>()
        override __.EvaluateDistance(x : single, y : single) =
            try
                x - y |> Convert.ToDecimal |> Math.Abs
            with
            | :? OverflowException -> Decimal.MaxValue

type DoubleSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<double>()
        override __.EvaluateDistance(x : double, y : double) =
            try
                x - y |> Convert.ToDecimal |> Math.Abs
            with
            | :? OverflowException -> Decimal.MaxValue

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