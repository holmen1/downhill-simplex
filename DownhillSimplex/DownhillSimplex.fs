namespace DownhillSimplex.FSharp

type Vertex =
    | Vertex of float list

    static member (+)(Vertex u, Vertex v) = List.map2 (+) u v |> Vertex
    static member (-)(Vertex u, Vertex v) = List.map2 (-) u v |> Vertex
    static member (*)(a: float, Vertex v) = List.map (fun x -> a * x) v |> Vertex
    static member (/)(Vertex v, a: float) = List.map (fun x -> x / a) v |> Vertex

// Nelder-Mead
[<AbstractClass>]
type DownhillSimplex(init: Vertex) =

    let alpha = 1.0 // reflection coefficient
    let gamma = 2.0 // expansion -""-
    let rho = 0.5 // contraction -""-
    let bumpfactor = 1.2 // initial vertex perturbation

    let toList (Vertex v) = v
    let Length (Vertex v) = v.Length

    let log = System.Math.Log
    let exp = System.Math.Exp

    // bumps v[index] -> f(v[index])
    let bump (index: int) (f: float -> float) (Vertex v) =
        List.mapi (fun i x -> if i = index then f x else x) v |> Vertex

    let reflection (xc: Vertex) (xh: Vertex) = xc + alpha * (xc - xh)

    let expansion (x': Vertex) (xc: Vertex) = x' + gamma * (x' - xc)

    let contraction (xc: Vertex) (xh: Vertex) = xc - rho * (xc - xh)

    let shrink (l: int) (simplex: Vertex list) =
        let xl = simplex.Item(l)
        List.map (fun v -> contraction xl v) simplex

    let rec remove (i: int) simplex =
        match i, simplex with
        | 0, _ :: xs -> xs
        | i, x :: xs -> x :: remove (i - 1) xs
        | _, [] -> failwith "index out of range"

    let makeSimplex (v: Vertex) =
        let n = Length v
        v :: List.init n (fun i -> bump i (fun x -> 1.2 * x) v)

    // argMax/argMin
    // returns (i, f(vertex_i))
    let argMax (f: Vertex -> float) (simplex: Vertex list) =
        List.mapi (fun i v -> (i, f v)) simplex |> List.maxBy snd

    let argMin (f: Vertex -> float) (simplex: Vertex list) =
        List.mapi (fun i v -> (i, f v)) simplex |> List.minBy snd

    // arithmetic mean vertex of simplex
    let centroid (simplex: Vertex list) =
        let rec sumSimplex (simplex: Vertex list) =
            match simplex with
            | head :: tail when tail.IsEmpty -> head
            | head :: tail -> head + (sumSimplex tail)
            | [] -> failwith "warning FS0025"

        (sumSimplex simplex) / (float simplex.Length)


    // each step of the method consists in an update of the current simplex
    // these updates are carried out using four operations
    // reflection, expansion, contraction, and multiple shrinking
    let downhill (objFn: Vertex -> float) (simplex: Vertex list) (tol: float) =
        let h, fhigh = argMax objFn simplex
        let xh = simplex.Item(h)
        let simplex' = remove h simplex
        let _, fh' = argMax objFn simplex'
        let l, flow = argMin objFn simplex'
        let xc = centroid simplex'
        let x' = reflection xc xh
        let f' = objFn x' // value on reflected point
        //if (fhigh - flow) < tol then (simplex, true) // converged
        if flow < tol then
            (simplex, true) // converged
        elif f' < flow then
            let x'' = expansion x' xc
            let f'' = objFn x''

            if f'' < flow then
                (x'' :: simplex', false)
            else
                (x' :: simplex', false)
        elif f' > fh' then
            if f' <= fhigh then
                (x' :: simplex', false)
            else
                let x'' = contraction xc xh
                let f'' = objFn x''

                if f'' > fhigh then
                    (shrink l simplex, false)
                else
                    (x'' :: simplex', false)
        else
            (x' :: simplex', false)

    abstract member objective: Vertex -> float
    member this.obj(vs: float list) = Vertex vs |> this.objective

    // convergence criterion on cost function
    abstract member tol: float
    default this.tol = 1E-6

    member this.initGuess = init

    member this.fit(v: Vertex) =
        let maxiter = 5000
        let mutable iter = 0
        let mutable simplex = makeSimplex v
        let mutable converged = false

        while (not converged && iter < maxiter) do
            let s, c = downhill this.objective simplex this.tol
            simplex <- s
            converged <- c
            iter <- iter + 1

        let l, flow = argMin this.objective simplex
        let res = simplex.Item(l)
        toList res
