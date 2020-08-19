type Vertex = Vertex of float list

// bumps v[index] -> f(v[index])
let bump index f v = 
    (fun (Vertex u) -> u) v
    |> List.mapi (fun i x -> if i = index then f x else x)
    |> Vertex

let rec remove index v =
    match index, v with
    | 0, _::xs -> xs
    | index, x::xs -> x::remove (index - 1) xs
    | _, [] -> failwith "index out of range"

// initial simplex
let makeSimplex vertex =
    let (Vertex l) = vertex
    let n = l.Length
    vertex :: List.init n (fun index -> bump index (fun f -> 1.1 * f) vertex)

// binary operator, o = +, -, /
let op o (Vertex v1) (Vertex v2) =
    List.map2 o v1 v2
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

// main
// fit bananaFcn [3.0; 5.0]
// val it : float list = [1.0; 1.0]
let fit objective init =
    let simplex = makeSimplex init
    let l, flow = argMin objective simplex
    let h, fhigh = argMax objective simplex
    let xc = centroid simplex
    simplex.Item(l), flow

// to test
let objFcn vertex =
    let (Vertex l) = vertex
    let bananaFcn ((a,b): float*float) ((x,y): float*float) =
        (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
    bananaFcn (1.0, 100.0) (l.Item(0), l.Item(1))


// - - - - 

let simplex = List.map Vertex [[-1.0; 0.0]; [0.0; 1.0]; [1.0; 0.0]]
let l, flow = argMin objFcn simplex
let h, fhigh = argMax objFcn simplex
let xc = centroid simplex
let xh = simplex.Item(1)

op (-) xc xc
simplex.Item(h) 
let x' = op (+) xc (op (-) xc xh)