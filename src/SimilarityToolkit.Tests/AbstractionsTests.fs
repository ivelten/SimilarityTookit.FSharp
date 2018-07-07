module AbstractionsTests

open System
open SimilarityToolkit.Abstractions
open Xunit
open Assertions
open FsCheck.Xunit

type TestData =
    { Value : int }

type TestEvaluator() =
    inherit SimilarityEvaluatorBase<TestData>()
    override __.EvaluateDistance(x : TestData, y : TestData) =
        x.Value - y.Value |> Math.Abs |> Convert.ToDecimal

[<Fact>]
let ``SimilarityEvaluatorBase<'T>: evaluated type should be the type of generic argument`` () =
    TestEvaluator().EvaluatedType |> equals typeof<TestData>

[<Property>]
let ``SimilarityEvaluatorBase<'T>: Non-generic EvaluateDistance should call generic EvaluateDistance`` (x : TestData) (y : TestData) =
    let evaluator = TestEvaluator()
    evaluator.EvaluateDistance(x, y) |> equals <| evaluator.EvaluateDistance(box x, box y)