// Test driven development
// Using Rosenbrock's banana function

let bananaFcn ((a,b): float*float) ((x,y): float*float) =
    (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0

let objFcn = bananaFcn (1.0, 100.0)

// Returns list [(f(x1), x1); (f(x2), x2); ...; (f(xn+1), xn+1)]
// s. t. f(x1) <= f(x2) <= ... <= f(xn+1)
let orderVertices valueVertexPairs =
    valueVertexPairs
    |> List.sortBy fst


let fit objective initGuess =
    // to be implemented ...
    initGuess

// TESTS ---- given-when-then ----

// Sort
let t0 = 0.23, (0.0, 9.99)
let t1 = 1.23, (0.0, 9.99)
let t2 = 2.23, (0.0, 9.99)
let t3 = 3.23, (0.0, 9.99)
let valueList = [t2; t3; t0; t1]
let sortedList = orderVertices valueList
printfn "Passed the sorting-test : %b" (sortedList = [t0; t1; t2; t3])

// Main
let init = (-0.5, 3.0)
let result = fit objFcn init
let success = result = (1.0, 1.0)
printfn "Passed the banana-test : %b" success










