open Microsoft.FSharp.Collections
open Microsoft.FSharp.Collections
// Test driven development
// Using Rosenbrock's banana function

let bananaFcn ((a,b): float*float) ((x,y): float*float) =
    (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0

let objFcn =
    let bananaFcn ((a,b): float*float) ((x,y): float*float) =
        (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
    bananaFcn (1.0, 100.0)

// Bumps x[index] -> f(x[index])
let bumpVertex index f vertex = 
    List.mapi (fun i x -> if i = index then f x else x) vertex

let initializeVertices (initVertex:float list) =
    let n = initVertex.Length
    initVertex :: List.init n (fun index -> bumpVertex index (fun f -> 1.1 * f) initVertex)


let t = (1,2,3,4)


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

let maxIndex0 vertexVals =
    vertexVals
    |> Seq.mapi (fun i v -> i, v)
    |> Seq.maxBy snd

let argMax f vertices =
    vertices
    |> List.mapi (fun i v -> (i, f v))
    |> List.maxBy snd

let argMin f vertices =
    vertices
    |> List.mapi (fun i v -> (i, f v))
    |> List.minBy snd
// argMax (fun [x; y] -> 4.0 - x ** 2.0 + y ** 2.0) [[0.0;0.0];[4.0;4.0];[1.0;2.0];[88.0;88.0]];;
// val it : int * float = (2, 7.0)


(* let fit objective initGuess =
    // to be implemented ...
    initGuess *)

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



let addera = (fun [x:float; y:float] -> x + y)
//let addera = (fun [x; y] -> x + y)


type Vertex = float list
let dim (vertex:Vertex) = 
    vertex.Length
// Bumps x[index] -> f(x[index])
let bump index f vertex = 
    List.mapi (fun i x -> if i = index then f x else x) vertex
// > dim [1.0; 2.2; 5.5];;                                         
// val it : int = 3
// > bump 1 (fun x -> 2.0 * x) [1.0; 2.2; 5.5];;
// val it : float list = [1.0; 4.4; 5.5]

type List<'a> with
    member this.Dim = this.Length



let fit objective initGuess =
    let vertices = initializeVertices initGuess
    let highVertex = argMax (fun [x; y] -> x + y) vertices
    let lowVertex = argMin (fun [x; y] -> x + y) vertices
    initGuess

let listToTuple l =
    let l' = List.toArray l
    let types = l' |> Array.map (fun o -> o.GetType())
    let tupleType = Microsoft.FSharp.Reflection.FSharpType.MakeTupleType types
    Reflection.FSharpValue.MakeTuple (l', tupleType)

let toTuple2 l =
    match l with
    | [x; y] -> x, y
    | _ -> failwith "List not 2-d"









