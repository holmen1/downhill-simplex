



namespace DownhillSimplex.FSharp
  type Vertex =
    | Vertex of float list
    with
      static member ( + ) : Vertex * Vertex -> Vertex
      static member ( / ) : Vertex * a:float -> Vertex
      static member ( * ) : a:float * Vertex -> Vertex
      static member ( - ) : Vertex * Vertex -> Vertex
    end
  [<AbstractClassAttribute ()>]
  type DownhillSimplex =
    class
      new : init:Vertex -> DownhillSimplex
      abstract member cost : Vertex -> float
      member fit : Vertex * float * int * bool
    end
  type MinimizeBanana =
    class
      inherit DownhillSimplex
      new : x:float * y:float -> MinimizeBanana
      override cost : Vertex -> float
    end
  type MinimizeHimmelblau =
    class
      inherit DownhillSimplex
      new : x:float * y:float -> MinimizeHimmelblau
      override cost : Vertex -> float
    end

