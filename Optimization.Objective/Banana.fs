﻿namespace Optimization.Objective

(* 
    Light version of
    http://fssnip.net/X/title/Triple-3D-Vector
 *)

type Vertex(x: float, y: float) =
  member this.X = x
  member this.Y = y
  
  static member (+) (u: Vertex, v: Vertex) = Vertex(u.X + v.X, u.Y + v.Y)
  static member (+) (v: Vertex, a: float) = Vertex(a + v.X, a + v.Y)
  static member (-) (u: Vertex, v: Vertex) = Vertex(u.X - v.X, u.Y - v.Y)
  static member (-) (v: Vertex, a: float) = Vertex(v.X - a, v.Y - a)
  static member ( * ) (a: float, v: Vertex) = Vertex(a * v.X, a * v.Y)
  static member ( / ) (v: Vertex, a: float) = Vertex(v.X / a, v.Y / a)
  static member toVertex (x: float, y: float) = Vertex(x, y)
  member this.toTuple = x, y

  override this.Equals(ob : obj) =
    match ob with
    | :? Vertex as other -> other.X = this.X && other.Y = this.Y
    | _ -> false
  override this.GetHashCode() =
    let hash = 23.0
    let hash = hash * 31.0 + this.X
    int (hash * 31.0 + this.Y)


module Objective =
    let Fcn (v: Vertex) =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        bananaFcn (1.0, 100.0) v.toTuple


module Vertex =
  let alpha = 1.0       // reflection coefficient
  let gamma = 2.0       // expansion -""-
  let rho = 0.5         // contraction -""-
  let bumpfactor = 1.2  // initial vertex perturbation

  let toTuple (v: Vertex) = v.X, v.Y
  let Length (v: Vertex) = 2
  let mapi (i: int) (f: float->float) (v: Vertex) =
    match i with
      | 0 -> Vertex(f v.X, v.Y)
      | 1 -> Vertex(v.X, f v.Y)
      | _ -> sprintf "invalid index %d in Vertex" i |> failwith
  
  // bumps v[index] -> f(v[index])
  let bump (i: int) (f: float->float) (v: Vertex) = 
      mapi i f v

  let reflection (xc: Vertex) (xh: Vertex) =
      xc + alpha * (xc - xh)

  let expansion (x': Vertex) (xc: Vertex) =
      x' + gamma * (x' - xc)

  let contraction (xc: Vertex) (xh: Vertex) =
      xc - rho * (xc - xh)

  let shrink (l: int) (simplex: Vertex list) =
    let xl = simplex.Item(l)
    List.map (fun v -> contraction xl v) simplex


(*  Simplex *)

  let rec remove (i: int) simplex =
      match i, simplex with
      | 0, _::xs -> xs
      | i, x::xs -> x::remove (i - 1) xs
      | _, [] -> failwith "index out of range"

  // initial simplex
  let makeSimplex (v: Vertex) =
      let n = Length v
      v :: List.init n (fun i -> bump i (fun x -> 1.2 * x) v)

  // argMax/argMin
  // returns (i, f(vertex_i))
  let argMax (f: Vertex -> float) (simplex: Vertex list) =
      List.mapi (fun i v -> (i, f v)) simplex
      |> List.maxBy snd

  let argMin (f: Vertex -> float) (simplex: Vertex list) =
      List.mapi (fun i v -> (i, f v)) simplex
      |> List.minBy snd

  // arithmetic mean vertex of simplex
  let centroid (simplex: Vertex list) =
      let rec sumSimplex (simplex: Vertex list) =
          match simplex with
              | head :: tail  when tail.IsEmpty -> head
              | head :: tail -> head + (sumSimplex tail)
              | [] -> failwith "warning FS0025"
      (sumSimplex simplex) / (float simplex.Length)

