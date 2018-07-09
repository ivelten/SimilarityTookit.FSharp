namespace SimilarityToolkit.Abstractions

open System

[<AbstractClass>]
type SimilarityEvaluatorBase() =
    abstract EvaluatedType : Type
    abstract EvaluateDistance : obj * obj -> decimal

[<AbstractClass>]
type SimilarityEvaluatorBase<'T>() =
    inherit SimilarityEvaluatorBase()
    abstract EvaluateDistance : 'T * 'T -> decimal
    override this.EvaluateDistance(x : obj, y : obj) =
        this.EvaluateDistance(x :?> 'T, y :?> 'T)
    override __.EvaluatedType = typeof<'T>

type NullableSimilarityEvaluator<'T when 'T : struct and 'T : (new : unit -> 'T) and 'T :> ValueType>(evaluator : SimilarityEvaluatorBase<'T>) =
    inherit SimilarityEvaluatorBase<Nullable<'T>>()
    override __.EvaluateDistance(x : Nullable<'T>, y : Nullable<'T>) =
        let x = box <| if x.HasValue then x.Value else Unchecked.defaultof<'T>
        let y = box <| if y.HasValue then y.Value else Unchecked.defaultof<'T>
        evaluator.EvaluateDistance(x, y)