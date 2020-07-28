namespace DownhillSimplex

// Nelder-Mead
module NM =

    // Bumps x[index] -> f(x[index])
    let bumpVertex index f vertex = 
        List.mapi (fun i x -> if i = index then f x else x) vertex

    let initializeVertices (initVertex:float list) =
        let n = initVertex.Length
        initVertex :: List.init n (fun index -> bumpVertex index (fun f -> 1.1 * f) initVertex)

    // arithmetic mean vertex of all vertices
    let centroid (vertices:float list list) =
        let rec sumCoordinates (v:float list list) =
            match v with
                | head :: tail  when tail.IsEmpty -> List.map2 (+) head [ for i in 1 .. head.Length -> 0.0 ]
                | head :: tail -> List.map2 (+) head (sumCoordinates tail)
                | [] -> failwith "warning FS0025"
        sumCoordinates vertices
        |> List.map (fun x ->  x / float vertices.Length)

    let evaluateVertices f vertices =
        vertices
        |> List.zip (List.map f vertices)

    let argMax f vertices =
        vertices
        |> List.mapi (fun i v -> (i, f v))
        |> List.maxBy snd

    // Returns list with tuples [(f(x1), x1); (f(x2), x2); ...; (f(xn+1), xn+1)]
    // s. t. f(x1) <= f(x2) <= ... <= f(xn+1)
    let orderVertices valueVertexPairs:(float*'a) list =
        valueVertexPairs
        |> List.sortBy fst

    let toTuple2 l =
        match l with
        | [x; y] -> x, y
        | _ -> failwith "List not 2-d"

    let objFcn =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0)

    let fit objective initGuess =
        // to be implemented ...
        initGuess