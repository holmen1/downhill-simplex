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

    let expansion (xc: Vertex) (x': Vertex) =
        x' + 2.0 * (x' - xc)

    // argMax/argMin
    // returns (i, f(vertex_i))
    let argMax (f: Vertex -> float) (simplex: Vertex list) =
        List.mapi (fun i v -> (i, f v)) simplex
        |> List.maxBy snd

    let argMin f simplex =
        List.mapi (fun i v -> (i, f v)) simplex
        |> List.minBy snd

    (*
    let downhillLoop objective simplex =
        let l, flow = argMin objective simplex
        let h, _ = argMax objective simplex
        let xh = simplex.Item(h)
        let simplex' = remove h simplex
        let xc = centroid simplex'
        let xr = reflection xc xh
        let fr = objective xr
        let x =
            if fr < flow
                then expansion xc xr
            elif fr > flow then xr 
            else xc
        x::simplex'

*)

    // to test
    let objFcn (v: Vertex) =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0) v.toTuple
(*
    // main
    // fit bananaFcn [3.0; 5.0]
    // val it : float list = [1.0; 1.0]
    let fit init =
        let simplex = makeSimplex init
        downhillLoop objFcn simplex
*)

