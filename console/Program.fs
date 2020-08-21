open System
open Vertex2D

[<EntryPoint>]
let main argv =
    let tr1 = vertex(5.0, 2.0)
    let tr2 = vertex(5.0, 2.0)
    let v1 = tr1 + tr2
    printfn "Hello Vertex %A" v1
    0 // return an integer exit code
