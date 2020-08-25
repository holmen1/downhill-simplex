



namespace FSharp.Numerics
  type Vertex =
    class
      new : x:float * y:float -> Vertex
      override Equals : ob:obj -> bool
      override GetHashCode : unit -> int
      member Item : i:int -> float
      override ToString : unit -> string
      member X : float
      member Y : float
      member norm : float
      member toArray : float []
      member toList : float list
      member toTuple : float * float
      static member Zero : unit -> Vertex
      static member ( + ) : a:float * b:Vertex -> Vertex
      static member ( + ) : a:Vertex * b:float -> Vertex
      static member ( + ) : a:Vertex * b:Vertex -> Vertex
      static member ( / ) : a:Vertex * b:float -> Vertex
      static member ( ./ ) : a:Vertex * b:Vertex -> Vertex
      static member ( .* ) : a:Vertex * b:Vertex -> Vertex
      static member ( * ) : a:float * b:Vertex -> Vertex
      static member ( * ) : a:Vertex * b:Vertex -> float
      static member ( - ) : a:float * b:Vertex -> Vertex
      static member ( - ) : a:Vertex * b:float -> Vertex
      static member ( - ) : a:Vertex * b:Vertex -> Vertex
      static member toVertex : x:float * y:float -> Vertex
    end
  module Vertex = begin
    val toList : v:Vertex -> float list
    val toTuple : v:Vertex -> float * float
    val dot : a:Vertex -> b:Vertex -> float
    val norm : v:Vertex -> float
    val Length : v:Vertex -> int
    val Zero : Vertex
    val Ones : Vertex
    val exists : fn:(float -> bool) -> v:Vertex -> bool
    val map : f:(float -> float) -> v:Vertex -> Vertex
    val mapi : i:int -> f:(float -> float) -> v:Vertex -> Vertex
    val map2 : f:(float -> float -> float) -> a:Vertex -> b:Vertex -> Vertex
    val normalize : v:Vertex -> Vertex
    val init : f:(int -> float) -> Vertex
  end

