namespace FSharp.Numerics
(* 
    Light version of
    http://fssnip.net/X/title/Triple-3D-Vector
 *)

type Vertex(x: float, y: float) =
  member this.X = x
  member this.Y = y
 
  // new (x: int, y: int, z: int) = Vertex(float x, float y)
  
  static member ( * ) (a, b: Vertex) = Vertex(a * b.X, a * b.Y)
  static member ( * ) (a: Vertex, b: Vertex) = a.X * b.X + a.Y * b.Y
  static member ( .* ) (a: Vertex, b: Vertex) = Vertex(a.X * b.X, a.Y * b.Y)

  static member ( / ) (a: Vertex, b) = Vertex(a.X / b, a.Y / b)
  static member ( ./ ) (a: Vertex, b: Vertex) = Vertex(a.X / b.X, a.Y / b.Y)

  static member (+) (a, b: Vertex) = Vertex(a + b.X, a + b.Y)
  static member (+) (a: Vertex, b) = Vertex(b + a.X, b + a.Y)
  static member (+) (a: Vertex, b: Vertex) = Vertex(a.X + b.X, a.Y + b.Y)

  static member (-) (a, b: Vertex) = Vertex(a - b.X, a - b.Y)
  static member (-) (a: Vertex, b) = Vertex(a.X - b, a.Y - b)
  static member (-) (a: Vertex, b: Vertex) = Vertex(a.X - b.X, a.Y - b.Y)

  static member toVertex (x: float, y: float) = Vertex(x, y)
  //static member toVertex (x: float list) = Vertex(x.[0], x.[1])
  static member Zero() = Vertex (0.0, 0.0)
  member this.toTuple = x, y
  member this.toList = [x;y]
  member this.toArray = [|x;y|]
  member this.norm = sqrt (this.X * this.X + this.Y * this.Y)

  member this.Item (i: int) =
    match i with
      | 0 -> x
      | 1 -> y
      | _ -> sprintf "invalid index %d in Vertex" i |> failwith
  
  override this.ToString() = sprintf "[%f; %f]" x y

  override this.Equals(ob : obj) =
    match ob with
    | :? Vertex as other -> other.X = this.X && other.Y = this.Y
    | _ -> false

module Vertex =
  let toList (v: Vertex) = [v.X; v.Y]
  let toTuple (v: Vertex) = v.X, v.Y
  let dot (a: Vertex) (b: Vertex) = a.X * b.X + a.Y * b.Y
  let norm (v: Vertex) = sqrt (v.X * v.X + v.Y * v.Y)
  let Length (v: Vertex) = 2
  let Zero = Vertex(0.0, 0.0)
  let Ones = Vertex(1.0, 1.0)
  let exists fn (v: Vertex) =
    if fn v.X then true
    elif fn v.Y then true
    else false
  let map (f: float->float) (v: Vertex) = Vertex(f v.X, f v.Y)
  let mapi (i: int) (f: float->float) (v: Vertex) =
    match i with
      | 0 -> Vertex(f v.X, v.Y)
      | 1 -> Vertex(v.X, f v.Y)
      | _ -> sprintf "invalid index %d in Vertex" i |> failwith
  let map2 (f: float->float->float) (a: Vertex) (b: Vertex) = Vertex(f a.X b.X, f a.Y b.Y)
  let normalize (v: Vertex) = v / (norm v)
  let init (f: int->float) = Vertex(f 0, f 1)