open System
open FSharp.Numerics
open DownhillSimplex

[<EntryPoint>]
let main argv =
    let objFcn = NM.objFcn
    let simplex =
        List.map Vertex.toVertex [(-2.0, 2.0); (0.0, 5.0); (2.0, 2.0)]
    let expected = [(0.0, -1.0); (-2.0, 2.0); (2.0, 2.0)]
    let actual = NM.downhillLoop objFcn simplex |> List.map Vertex.toTuple
    printfn "Hello Vertex %A" actual
    0 // return an integer exit code
