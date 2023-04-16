



namespace DownhillSimplex.FSharp
    
    type Vertex =
        | Vertex of float list
        
        static member ( * ) : a: float * Vertex -> Vertex
        
        static member (+) : Vertex * Vertex -> Vertex
        
        static member (-) : Vertex * Vertex -> Vertex
        
        static member (/) : Vertex * a: float -> Vertex
    
    [<AbstractClass>]
    type DownhillSimplex =
        
        new: init: Vertex -> DownhillSimplex
        
        member fit: v: Vertex -> float list
        
        member obj: vs: float list -> float
        
        abstract objective: Vertex -> float
        
        member initGuess: Vertex
        
        abstract tol: float
        
        override tol: float

namespace DownhillSimplex.FSharp
    
    type OptimalDiscountFactors =
        inherit DownhillSimplex
        
        new: p1: float * p2: float * p3: float * p4: float * p5: float *
             p6: float * p7: float * p8: float * p9: float * p10: float *
             p12: float * p15: float * p20: float -> OptimalDiscountFactors
        
        member discountFactor: dur: int -> float
        
        member fiRate: dur: int -> float
        
        override objective: Vertex -> float
        
        override tol: float

namespace DownhillSimplex.FSharp
    
    type MinimizeBanana =
        inherit DownhillSimplex
        
        new: x: float * y: float -> MinimizeBanana
        
        override objective: Vertex -> float

