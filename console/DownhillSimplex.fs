namespace DownhillSimplex

// Nelder-Mead
module NM =

    // Bumps x[index] -> f(x[index])
    let bumpVertex index f vertex = 
        List.mapi (fun i x -> if i = index then f x else x) vertex

    let initializeVertices (initVertex:float list) =
        let n = initVertex.Length
        initVertex :: List.init n (fun index -> bumpVertex index (fun f -> 1.1 * f) initVertex)

    // Returns list [(f(x1), x1); (f(x2), x2); ...; (f(xn+1), xn+1)]
    // s. t. f(x1) <= f(x2) <= ... <= f(xn+1)
    let orderVertices valueVertexPairs:(float*'a) list =
        valueVertexPairs
        |> List.sortBy fst


    let fit objective initGuess =
        // to be implemented ...
        initGuess