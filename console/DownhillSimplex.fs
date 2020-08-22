namespace DownhillSimplex
open FSharp.Numerics

// Nelder-Mead
module NM =

    // bumps v[index] -> f(v[index])
    let bump (i: int) (f: float->float) (v: Vertex) = 
    //let bump i f v =
        Vertex.mapi i f v
        
    let rec remove (i: int) (simplex: Vertex list) =
        match i, simplex with
        | 0, _::xs -> xs
        | i, x::xs -> x::remove (i - 1) xs
        | _, [] -> failwith "index out of range"

    // initial simplex
    let makeSimplex (v: Vertex) =
        let n = Vertex.Length v
        v :: List.init n (fun i -> bump i (fun x -> 1.1 * x) v)
(*
    // binary operator, o = +, -, /
    let op o (Vertex v1) (Vertex v2) =
        List.map2 o v1 v2
        |> Vertex

    let scalmul a (Vertex v1) =
        List.map (fun x -> a * x) v1
        |> Vertex

    // zero Vertex of with v.Length
    let zero (Vertex v) =
        [ for i in 1 .. v.Length -> 0.0 ]
        |> Vertex

    // sum elementwise
    // sumVertices [[1.0; 1.0]; [2.0; 3.0]];;
    // val it : float list = [3.0; 4.0]
    let rec sumSimplex simplex =
        match simplex with
            | head :: tail  when tail.IsEmpty -> op (+) head (zero head)
            | head :: tail -> op (+) head (sumSimplex tail)
            | [] -> failwith "warning FS0025"

    // arithmetic mean vertex of simplex
    let centroid simplex =
        let (Vertex l) = sumSimplex simplex
        List.map (fun x ->  x / float simplex.Length) l
        |> Vertex

    let reflection xc xh =
        op (+) xc (op (-) xc xh)

    let expansion xc x' =
        op (+) x' (scalmul 2.0 (op (-) x' xc))

    // argMax/argMin
    // returns (i, f(vertex_i))
    let argMax f simplex =
        simplex
        |> List.mapi (fun i v -> (i, f v))
        |> List.maxBy snd

    let argMin f simplex =
        simplex
        |> List.mapi (fun i v -> (i, f v))
        |> List.minBy snd

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



    // to test
    let objFcn vertex =
        let (Vertex l) = vertex
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0) (l.Item(0), l.Item(1))

    // main
    // fit bananaFcn [3.0; 5.0]
    // val it : float list = [1.0; 1.0]
    let fit init =
        let simplex = makeSimplex init
        downhillLoop objFcn simplex *)

