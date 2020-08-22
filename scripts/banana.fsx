
// Nelder-Mead
type Vertex = Vertex of float list

// bumps v[index] -> f(v[index])
let bump index f v =
    (fun (Vertex u) -> u) v
    |> List.mapi (fun i x -> if i = index then f x else x)
    |> Vertex

let v = [1.0; 2.0; 3.0] |> Vertex
let expected = [1.0; 2.2; 3.0] |> Vertex
let v' = bump 1 (fun x -> 1.1 * x) v
let (Vertex vlist) = v'

// initial simplex
let makesimplex v =
    let (Vertex l) = v
    let n = l.Length
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
let tuple_old (Vertex v) =
    match v with
    | [x; y] -> x, y
    | _ -> failwith "List not 2-d"

let tuple (Vertex v) =
    let v' = List.toArray v |> Array.map box
    let types = v' |> Array.map (fun o -> o.GetType())
    let tupleType = Microsoft.FSharp.Reflection.FSharpType.MakeTupleType types
    Reflection.FSharpValue.MakeTuple (v', tupleType)
    |> unbox<float*float>

let a = List.toArray [1.0; 2.0; 3.0]
let types = a |> Array.map (fun o -> o.GetType())
let tupleType = Microsoft.FSharp.Reflection.FSharpType.MakeTupleType types
let a' = Array.map box a
let o = Reflection.FSharpValue.MakeTuple (a' , tupleType)
let t = unbox<float*float*float> o


let u = [1.0; 2.0] |> Vertex
let u' = tuple u



// main
// fit bananaFcn [3.0; 5.0]
// val it : float list = [1.0; 1.0]
let fit objective init =
    let tuples =
        makesimplex init
        |> List.map tuple
    let low = argMin objective tuples
    tuples.Item(fst low)

// to test
let objFcn =
    let bananaFcn ((a,b): float*float) ((x,y): float*float) =
        (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
    bananaFcn (1.0, 100.0)


let rec remove i l =
    match i, l with
    | 0, x::xs -> xs
    | i, x::xs -> x::remove (i - 1) xs
    | i, [] -> failwith "index out of range"

// - - -











