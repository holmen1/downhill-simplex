



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
      abstract member tol : float
      abstract member objective : Vertex -> float
      member fit : v:Vertex -> float list
      member initGuess : Vertex
      override tol : float
      member obj : vs:float list -> float
    end

namespace DownhillSimplex.FSharp
  type OptimalDiscountFactors =
    class
      inherit DownhillSimplex
      new : p1:float * p2:float * p3:float * p4:float * p5:float * p6:float *
            p7:float * p8:float * p9:float * p10:float * p12:float * p15:float *
            p20:float -> OptimalDiscountFactors
      member discountFactor : dur:int -> float
      member fiRate : dur:int -> float
      override tol : float
      override objective : Vertex -> float
    end

namespace DownhillSimplex.FSharp
  type MinimizeBanana =
    class
      inherit DownhillSimplex
      new : x:float * y:float -> MinimizeBanana
      override objective : Vertex -> float
    end

