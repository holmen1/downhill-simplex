open System
open Optimization.Objective
open DownhillSimplex

[<EntryPoint>]
let main argv =
    let initVertex = Vertex(2.0, 3.0)
    let actual = NM.fit //initVertex
    printfn "Hello Vertex %A" actual
    0 // return an integer exit code
