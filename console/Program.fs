open System
open Triple

[<EntryPoint>]
let main argv =
    let tr1 = triple(5,2,3)
    let tr2 = triple(5,2,3)
    let v1 = (-tr1 + tr2 - tr1) .* tr2 ./ tr1 .** 2. * tr1 + tr1.[0] + tr2.X
    printfn "Hello Triple %A" v1
    0 // return an integer exit code
