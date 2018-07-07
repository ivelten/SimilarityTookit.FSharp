module Assertions

open Xunit

let equals (x : 'T) (y : 'T) = Assert.Equal<'T>(x, y)