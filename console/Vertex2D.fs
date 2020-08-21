//namespace DownhillSimplex
module Vertex2D
(* 
    Light version of
    http://fssnip.net/X/title/Triple-3D-Vector
 *)

type vertex(x: float, y: float) =
  member o.X = x
  member o.Y = y
  
  // new (x: int, y: int, z: int) = vertex(float x, float y)
  
  static member ( * ) (a, b: vertex) = vertex(a * b.X, a * b.Y)
  static member ( * ) (a: vertex, b: vertex) = a.X * b.X + a.Y * b.Y
  static member ( .* ) (a: vertex, b: vertex) = vertex(a.X * b.X, a.Y * b.Y)

  static member ( / ) (a: vertex, b) = vertex(a.X / b, a.Y / b)
  static member ( ./ ) (a: vertex, b: vertex) = vertex(a.X / b.X, a.Y / b.Y)

  static member (+) (a, b: vertex) = vertex(a + b.X, a + b.Y)
  static member (+) (a: vertex, b) = vertex(b + a.X, b + a.Y)
  static member (+) (a: vertex, b: vertex) = vertex(a.X + b.X, a.Y + b.Y)

  static member (-) (a, b: vertex) = vertex(a - b.X, a - b.Y)
  static member (-) (a: vertex, b) = vertex(a.X - b, a.Y - b)
  static member (-) (a: vertex, b: vertex) = vertex(a.X - b.X, a.Y - b.Y)

  static member tovertex (x: float list) = vertex(x.[0], x.[1])
  static member get_Zero() = vertex (0.0, 0.0)
  member o.toTuple = x, y
  member o.toList = [x;y]
  member o.toArray = [|x;y|]
  member o.norm = sqrt (o.X * o.X + o.Y * o.Y)

  member o.Item (idx: int) =
    match idx with
      | 0 -> x
      | 1 -> y
      | _ -> sprintf "invalid index %d in vertex" idx |> failwith
  
  override o.ToString() = sprintf "[%f; %f]" x y

  override o.Equals(ob : obj) =
    match ob with
    | :? vertex as other -> other.X = o.X && other.Y = o.Y
    | _ -> false

module vertex =
  let toArray (t: vertex) = [| t.X; t.Y |]
  let toList (t: vertex) = [t.X; t.Y]
  let toTuple (t: vertex) = t.X, t.Y
  let dot (a: vertex) (b: vertex) = a.X * b.X + a.Y * b.Y
  let norm (t: vertex) = sqrt (t.X * t.X + t.Y * t.Y)
  let Length (v: vertex) = 2
  let Zero = vertex(0.0, 0.0)
  let Ones = vertex(1.0, 1.0)
  let exists fn (el: vertex) =
    if fn el.X then true
    elif fn el.Y then true
    else false
  let map (fn: float->float) (el: vertex) = vertex(fn el.X, fn el.Y)
  let mapi (i: int) (fn: float->float) (el: vertex) =
    match i with
      | 0 -> vertex(fn el.X, el.Y)
      | 1 -> vertex(el.X, fn el.Y)
      | _ -> sprintf "invalid index %d in vertex" i |> failwith
  let map2 (fn: float->float->float) (a: vertex) (b: vertex) = vertex(fn a.X b.X, fn a.Y b.Y)
  let normalize (t: vertex) = t / (norm t)
  let init (fn: int->float) = vertex(fn 0, fn 1)