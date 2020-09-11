namespace DownhillSimplex.FSharp

    type MinimizeBanana(x: float, y: float) =
        inherit DownhillSimplex(Vertex [x; y])
        override this.objective (Vertex v) =
            let x, y = v.Head, v.Tail.Head
            let bananaFcn ((a,b): float*float) ((x,y): float*float) =
                (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
            bananaFcn (1.0, 100.0) (x, y)
            
