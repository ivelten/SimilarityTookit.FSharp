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