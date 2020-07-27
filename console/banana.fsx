// Test driven development
// Using Rosenbrock's banana function

let bananaFcn ((a,b): float*float) ((x,y): float*float) =
    (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0

let objFcn = bananaFcn (1.0, 100.0)

// Bumps x[index] -> f(x[index])
let bumpVertex index f vertex = 
    List.mapi (fun i x -> if i = index then f x else x) vertex

let initializeVertices (initVertex:float list) =
    let n = initVertex.Length
    initVertex :: List.init n (fun index -> bumpVertex index (fun f -> 1.1 * f) initVertex)


// let centroid (vertices:float list list) =
//     //let l = vertices.Head.Length
//     let array = Array.create vertices.Head.Length 0.0
//     let array0 = List.toArray vertices.Head
//     array0

let centroid3 (vertices:float list list) =
    List.map2 (+) vertices.Head vertices.Head


let rec centroid2 v=
    match v with
        | head :: tail -> head + centroid2 tail
        | [] -> 0


// let rec centroid (vertices:float list list)=
//     match vertices with
//         | head :: tail  when tail.IsEmpty -> List.map2 (+) head [ for i in 1 .. head.Length -> 0.0 ]
//         | head :: tail -> List.map2 (+) head (centroid tail)
//         | [] -> [ for i in 1 .. vertices.Head.Length -> 0.0 ]
//     |> List.map (fun x ->  x + 1.0)

let centroid (vertices:float list list) =
    let rec sumCoordinates (v:float list list) =
        match v with
            | head :: tail  when tail.IsEmpty -> List.map2 (+) head [ for i in 1 .. head.Length -> 0.0 ]
            | head :: tail -> List.map2 (+) head (sumCoordinates tail)
            | [] -> failwith "Hoppsan!"
    sumCoordinates vertices
    |> List.map (fun x ->  x / float vertices.Length)

    // (List.rev vertices).Tail
    // |> List.rev
    // |> sumCoordinates
    // |> List.map (fun x ->  x / float (vertices.Length - 1))
        
    

    
  



let evaluateVertices f vertices =
    vertices
    |> List.zip (List.map f vertices)




// Returns list [(f(x1), x1); (f(x2), x2); ...; (f(xn+1), xn+1)]
// s. t. f(x1) <= f(x2) <= ... <= f(xn+1)
let orderVertices valueVertexPairs:(float*'a) list =
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




let bump x =
    1.1 * x


let addera = (fun [x:float; y:float] -> x + y)
//let addera = (fun [x; y] -> x + y)










