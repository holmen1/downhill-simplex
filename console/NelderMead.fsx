// Nelder-Mead
module NM =

    // bumps v[index] -> f(v[index])
    let bump index f v = 
        List.mapi (fun i x -> if i = index then f x else x) v

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




    // In Point2D, two immutable values are defined.
    // It also has a member which computes a distance between itself and another Point2D.
    // Point2D has an explicit constructor.
    // You can create zero-initialized instances of Point2D, or you can
    // pass in arguments to initialize the values.
    type Point2D =
        struct
            val X: float
            val Y: float
            new(x: float, y: float) = { X = x; Y = y }

            member this.GetDistanceFrom(p: Point2D) =
                let dX = (p.X - this.X) ** 2.0
                let dY = (p.Y - this.Y) ** 2.0
                dX + dY
                |> sqrt
        end

    type Vertex =
        struct
            val X: float
            val Y: float
            new(x: float, y: float) = { X = x; Y = y }

            member this.Dimension() =
                2.0
        end

    type Simplex =
        struct
            val V0: Vertex
            val V1: Vertex
            val V2: Vertex
            new(V: Vertex) = 
                { V0 = V
                  V1 = V
                  V2 = V }

            member this.Dimension() =
                2.0
        end