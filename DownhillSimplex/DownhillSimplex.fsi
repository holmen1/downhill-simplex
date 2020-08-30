



namespace DownhillSimplex.FSharp
  type Vertex =
    | Vertex of float list
    with
      static member ( + ) : Vertex * Vertex -> Vertex
      static member ( / ) : Vertex * a:float -> Vertex
      static member ( * ) : a:float * Vertex -> Vertex
      static member ( - ) : Vertex * Vertex -> Vertex
    end
  type DownhillSimplex =
    class
      new : init:Vertex -> DownhillSimplex
      new : x:float * y:float -> DownhillSimplex
      member fit : Vertex * int * bool
    end

