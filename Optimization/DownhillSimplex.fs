namespace DownhillSimplex
open FSharp.Numerics
open FSharp.Numerics.Vertex

// Nelder-Mead
module NM =

    // each step of the method consists in an update of the current simplex
    // these updates are carried out using four operations
    // reflection, expansion, contraction, and multiple shrinking
    let downhill (objFn: Vertex -> float) (simplex: Vertex list) =
        let h, fhigh = argMax objFn simplex
        let xh = simplex.Item(h)
        let simplex' = remove h simplex
        let _, fh' = argMax objFn simplex'
        let l, flow = argMin objFn simplex'
        let xc = centroid simplex'
        let x' = reflection xc xh
        let f' = objFn x' // value on reflected point
        if (fhigh - flow) < 1.0E-20 then (simplex, true) // converged
        elif f' < flow then
            let x'' = expansion x' xc
            let f'' = objFn x''
            if f'' < flow then (x''::simplex', false)
            else (x'::simplex', false)
        elif f' > fh' then
            if f' <= fhigh then (x'::simplex', false)
            else
                let x'' = contraction xc xh
                let f'' = objFn x''
                if f'' > fhigh then (shrink l simplex, false)
                else (x''::simplex', false)
        else (x'::simplex', false)

    // to test
    let objFcn (v: Vertex) =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0) v.toTuple

    // main
    let fit (init: Vertex) =
        let maxiter = 1000
        let mutable iter = 0
        let mutable simplex = makeSimplex init
        let mutable converged = false
        while (not converged && iter < maxiter) do
            let s, c = downhill objFcn simplex
            simplex <- s
            converged <- c
            iter <- iter + 1
        let l, flow = argMin objFcn simplex
        (simplex.Item(l).toTuple, flow, iter, converged)        

