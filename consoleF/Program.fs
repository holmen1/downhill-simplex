open System
open DownhillSimplex.FSharp

[<EntryPoint>]
let main argv =
    //let DS = DownhillSimplex(2.0, 3.0)
    let MB = MinimizeBanana(2.0, 3.0)
    let banana = MB.fit 
    printfn "fit -> %A" banana
    // fit -> (Vertex [1.000014292; 1.000027606], 2.998468649e-10, 65, true)
    let MH = MinimizeHimmelblau(2.0, 3.0)
    let himmelblau = MH.fit 
    printfn "fit -> %A" himmelblau
    // fit -> (Vertex [3.000001421; 2.0000037], 4.126256828e-10, 46, true)
    0 // return an integer exit code
