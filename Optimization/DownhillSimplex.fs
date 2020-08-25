namespace DownhillSimplex
open FSharp.Numerics

// Nelder-Mead
module NM =

    // bumps v[index] -> f(v[index])
    let bump (i: int) (f: float->float) (v: Vertex) = 
    //let bump i f v =
        Vertex.mapi i f v
        
    let rec remove (i: int) simplex =
        match i, simplex with
        | 0, _::xs -> xs
        | i, x::xs -> x::remove (i - 1) xs
        | _, [] -> failwith "index out of range"

    // initial simplex
    let makeSimplex (v: Vertex) =
        let n = Vertex.Length v
        v :: List.init n (fun i -> bump i (fun x -> 1.1 * x) v)

    // sum elementwise
    // sumVertices [[1.0; 1.0]; [2.0; 3.0]];;
    // val it : float list = [3.0; 4.0]
    let rec sumSimplex (simplex: Vertex list) =
        match simplex with
            | head :: tail  when tail.IsEmpty -> head
            | head :: tail -> head + (sumSimplex tail)
            | [] -> failwith "warning FS0025"

    // arithmetic mean vertex of simplex
    let centroid (simplex: Vertex list) =
        (sumSimplex simplex) / (float simplex.Length)
    
    let reflection (xc: Vertex) (xh: Vertex) =
        xc + (xc - xh)

    let expansion (x': Vertex) (xc: Vertex) =
        x' + 2.0 * (x' - xc)

    let contraction (xc: Vertex) (xh: Vertex) =
        xc - 0.5 * (xc - xh)

    // mid-point
    let mid (u: Vertex) (v: Vertex) =
        (u + v) / 2.0

    let shrink (i: int) (simplex: Vertex list) =
        let xl = simplex.Item(i)
        List.map (fun v -> mid xl v) simplex

    // argMax/argMin
    // returns (i, f(vertex_i))
    let argMax (f: Vertex -> float) (simplex: Vertex list) =
        List.mapi (fun i v -> (i, f v)) simplex
        |> List.maxBy snd

    let argMin f simplex =
        List.mapi (fun i v -> (i, f v)) simplex
        |> List.minBy snd

    let downhillLoop objective simplex =
        let h, fhigh = argMax objective simplex
        let xh = simplex.Item(h)
        let simplex' = remove h simplex
        let _, fh' = argMax objective simplex'
        let l, flow = argMin objective simplex'
        let xc = centroid simplex'
        let x' = reflection xc xh
        let f' = objective x'
        if f' < flow then
            let x'' = expansion x' xc
            let f'' = objective x''
            if f'' < flow then x''::simplex'
            else x'::simplex'
        elif f' > fh' then
            if f' <= fhigh then xh::simplex'
            else
                let x'' = contraction xc xh
                let f'' = objective x''
                if f'' > fhigh then shrink l simplex
                else x''::simplex'
        else x'::simplex'

    // to test
    let objFcn (v: Vertex) =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0) v.toTuple

    // main
    // fit bananaFcn [3.0; 5.0]
    // val it : float list = [1.0; 1.0]
    let fit init =
        let simplex = makeSimplex init
        downhillLoop objFcn simplex

