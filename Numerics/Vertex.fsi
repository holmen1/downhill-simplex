



namespace FSharp.Numerics
  type Vertex =
    class
      new : x:float * y:float -> Vertex
      override Equals : ob:obj -> bool
      override GetHashCode : unit -> int
      override ToString : unit -> string
      member X : float
      member Y : float
      member toTuple : float * float
      static member Zero : unit -> Vertex
      static member ( + ) : u:Vertex * v:Vertex -> Vertex
      static member ( + ) : v:Vertex * a:float -> Vertex
      static member ( / ) : v:Vertex * a:float -> Vertex
      static member ( * ) : a:float * v:Vertex -> Vertex
      static member ( - ) : u:Vertex * v:Vertex -> Vertex
      static member ( - ) : v:Vertex * a:float -> Vertex
      static member toVertex : x:float * y:float -> Vertex
    end
  module Vertex = begin
    val toTuple : v:Vertex -> float * float
    val norm : v:Vertex -> float
    val Length : v:Vertex -> int
    val Zero : Vertex
    val mapi : i:int -> f:(float -> float) -> v:Vertex -> Vertex
    val bump : i:int -> f:(float -> float) -> v:Vertex -> Vertex
    val reflection : xc:Vertex -> xh:Vertex -> Vertex
    val expansion : x':Vertex -> xc:Vertex -> Vertex
    val contraction : xc:Vertex -> xh:Vertex -> Vertex
    val mid : u:Vertex -> v:Vertex -> Vertex
    val remove : i:int -> simplex:'a list -> 'a list
    val makeSimplex : v:Vertex -> Vertex list
    val argMax : f:(Vertex -> float) -> simplex:Vertex list -> int * float
    val argMin : f:(Vertex -> float) -> simplex:Vertex list -> int * float
    val centroid : simplex:Vertex list -> Vertex
    val shrink : i:int -> simplex:Vertex list -> Vertex list
  end

