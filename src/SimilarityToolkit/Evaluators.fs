﻿namespace SimilarityToolkit.Evaluators

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

type StringSimilarityEvaluator() =
    inherit SimilarityEvaluatorBase<string>()
        override __.EvaluateDistance(x : string, y : string) =
            let length (x : string) =
                match x with
                | null -> 0
                | s -> s.Length
            if String.IsNullOrEmpty(x) && String.IsNullOrEmpty(y)
            then 0M
            else
                let len1 = length x
                let len2 = length y
                let distances = Array2D.zeroCreate<int> (len1 + 1) (len2 + 1)
                for i = 0 to len1 do distances.[i, 0] <- i
                for j = 0 to len2 do distances.[0, j] <- j
                for i = 1 to len1 do
                    for j = 1 to len2 do
                        let cost =  if y.[j - 1] = x.[i - 1] then 0 else 1
                        let a = Math.Min(distances.[i - 1, j] + 1, distances.[i, j - 1] + 1)
                        let b = distances.[i - 1, j - 1] + cost
                        distances.[i, j] <- Math.Min(a, b)
                distances.[len1, len2] |> Convert.ToDecimal