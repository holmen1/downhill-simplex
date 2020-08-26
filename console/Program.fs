open System
open FSharp.Numerics
open DownhillSimplex

[<EntryPoint>]
let main argv =
    let objFcn = NM.objFcn
    let initVertex = Vertex(2.0, 3.0)
    let actual = NM.fit initVertex
    printfn "Hello Vertex %A" actual
    0 // return an integer exit code
