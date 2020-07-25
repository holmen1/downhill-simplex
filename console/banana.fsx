// Test driven development
// Using Rosenbrock's banana function

let bananaFcn ((a,b): float*float) ((x,y): float*float) =
    (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0

let objFcn = bananaFcn (1.0, 100.0)

// Record to be sorted
type valuePair = {fx:float; x:(float*float)}
let v0 = {fx=0.23; x=(0.0, 9.99)}
let v1 = {fx=1.23; x=(1.0, 9.99)}
let v2 = {fx=2.31; x=(2.0, 9.99)}
let v3 = {fx=3.12; x=(3.0, 9.99)}
let valueList = [v2; v3; v0; v1]

valueList |> List.iter (fun s -> printf "fx: %f\n" s.fx)
printf "\n"
valueList |> List.sortBy (fun s -> s.fx)
|> List.iter (fun s -> printf "fx: %f\n" s.fx)


let fit objective initGuess =
    // to be implemented ...
    initGuess

// Test given-when-then
let init = (-0.5, 3.0)
let result = fit objFcn init
let success = result = (1.0, 1.0)
printfn "Passed the banana-test : %b" success