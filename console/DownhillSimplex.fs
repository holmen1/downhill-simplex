namespace DownhillSimplex


module DS =

    // Returns list [(f(x1), x1); (f(x2), x2); ...; (f(xn+1), xn+1)]
    // s. t. f(x1) <= f(x2) <= ... <= f(xn+1)
    let orderVertices valueVertexPairs =
        valueVertexPairs
        |> List.sortBy fst


    let fit objective initGuess =
        // to be implemented ...
        initGuess