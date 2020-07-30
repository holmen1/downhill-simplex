namespace DownhillSimplex

// Nelder-Mead
module NM =

    // bumps v[index] -> f(v[index])
    let bump index f v = 
        List.mapi (fun i x -> if i = index then f x else x) v

    let rec remove index v =
        match index, v with
        | 0, _::xs -> xs
        | index, x::xs -> x::remove (index - 1) xs
        | _, [] -> failwith "index out of range"

    // initial simplex
    let makesimplex (v:float list) =
        let n = v.Length
        v :: List.init n (fun index -> bump index (fun f -> 1.1 * f) v)

    // sum elementwise
    // sumVertices [[1.0; 1.0]; [2.0; 3.0]];;
    // val it : float list = [3.0; 4.0]
    let rec sumSimplex (simplex:float list list) =
        match simplex with
            | head :: tail  when tail.IsEmpty -> List.map2 (+) head [ for i in 1 .. head.Length -> 0.0 ]
            | head :: tail -> List.map2 (+) head (sumSimplex tail)
            | [] -> failwith "warning FS0025"

    // arithmetic mean vertex of simplex
    let centroid (simplex:float list list) =
        sumSimplex simplex
        |> List.map (fun x ->  x / float simplex.Length)

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

    // cast 2-d vertex to 2-d tuple
    let tuple v =
        match v with
        | [x; y] -> x, y
        | _ -> failwith "List not 2-d"

    // main
    // fit bananaFcn [3.0; 5.0]
    // val it : float list = [1.0; 1.0]
    let fit objective init =
        let tuples =
            makesimplex init
            |> List.map tuple
        let low = argMin objective tuples
        tuples.Item(fst low), snd low

    // to test
    let objFcn =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0)