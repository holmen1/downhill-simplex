open System
open DownhillSimplex.FSharp

[<EntryPoint>]
let main argv =
    let DS = DownhillSimplex(2.0, 3.0)
    let actual = DS.fit 
    printfn "fit -> %A" actual
    0 // return an integer exit code
// fit -> (Vertex [1.0; 1.0], 113, true)