open System
open DownhillSimplex.FSharp

[<EntryPoint>]
let main argv =
    //let DS = DownhillSimplex(2.0, 3.0)
    let MB = MinimizeBanana(2.0, 3.0)
    let actual = MB.fit 
    printfn "fit -> %A" actual
    0 // return an integer exit code
// fit -> (Vertex [1.0; 1.0], 4.344609959e-22, 113, true)