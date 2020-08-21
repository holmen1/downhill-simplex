//[<AutoOpen>]
module Triple

type triple(x: float, y: float, z: float) =
  member o.X = x
  member o.Y = y
  member o.Z = z
  
  new (x: int, y: int, z: int) = triple(float x, float y, float z)
  (*[omit:(A lot of code omitted :) )]*)
  static member ( * ) (a, b: triple) = triple(a * b.X, a * b.Y, a * b.Z)
  static member ( * ) (a, b: triple) =
    let af = float a
    triple(af * b.X, af * b.Y, af * b.Z)
  static member ( * ) (a: triple, b) = triple(b * a.X, b * a.Y, b * a.Z)
  static member ( * ) (a: triple, b) =
    let bf = float b
    triple(bf * a.X, bf * a.Y, bf * a.Z)
  static member ( * ) (a: triple, b: triple) = a.X * b.X + a.Y * b.Y + a.Z * b.Z
  static member ( .* ) (a: triple, b: triple) = triple(a.X * b.X, a.Y * b.Y, a.Z * b.Z)
  static member ( * ) (a: float [,], b: triple) = triple(a.[0,0] * b.X + a.[0,1] * b.Y + a.[0,2] * b.Z, a.[1,0] * b.X + a.[1,1] * b.Y + a.[1,2] * b.Z, a.[2,0] * b.X + a.[2,1] * b.Y + a.[2,2] * b.Z)

  static member ( / ) (a, b: triple) = triple(a / b.X, a / b.Y, a / b.Z)
  static member ( / ) (a, b: triple) =
    let af = float a
    triple(af / b.X, af / b.Y, af / b.Z)
  static member ( / ) (a: triple, b) = triple(a.X / b, a.Y / b, a.Z / b)
  static member ( / ) (a: triple, b) =
    let bf = float b
    triple(a.X / bf, a.Y / bf, a.Z / bf)
  static member ( ./ ) (a: triple, b: triple) = triple(a.X / b.X, a.Y / b.Y, a.Z / b.Z)

  static member (+) (a, b: triple) = triple(a + b.X, a + b.Y, a + b.Z)
  static member (+) (a, b: triple) =
    let af = float a
    triple(af + b.X, af + b.Y, af + b.Z)
  static member (+) (a: triple, b) = triple(b + a.X, b + a.Y, b + a.Z)
  static member (+) (a: triple, b) =
    let bf = float b
    triple(bf + a.X, bf + a.Y, bf + a.Z)
  static member (+) (a: triple, b: triple) = triple(a.X + b.X, a.Y + b.Y, a.Z + b.Z)

  static member (-) (a, b: triple) = triple(a - b.X, a - b.Y, a - b.Z)
  static member (-) (a, b: triple) =
    let af = float a
    triple(af - b.X, af - b.Y, af - b.Z)
  static member (-) (a: triple, b) = triple(a.X - b, a.Y - b, a.Z - b)
  static member (-) (a: triple, b) =
    let bf = float b
    triple(a.X - bf, a.Y - bf, a.Z - bf)
  static member (-) (a: triple, b: triple) = triple(a.X - b.X, a.Y - b.Y, a.Z - b.Z)

  static member (.**) (a: triple, b) = triple(a.X**b, a.Y**b, a.Z**b)

  static member (~-) (a: triple) = triple(-a.X, -a.Y, -a.Z)

  static member toTriple (x: int, y, z) = triple(x, y, z)
  static member toTriple (x: int list) = triple(x.[0], x.[1], x.[2])
  static member get_Zero() = triple (0.,0.,0.)
  member o.toTuple = x, y, z
  member o.toList = [x;y;z]
  member o.toArray = [|x;y;z|]
  member o.norm = sqrt (o.X * o.X + o.Y * o.Y + o.Z * o.Z)

  member o.Item (idx: int) =
    match idx with
      | 0 -> x
      | 1 -> y
      | 2 -> z
      | _ -> sprintf "invalid index %d in triple" idx |> failwith
  
  override o.ToString() = sprintf "[%f; %f; %f]" x y z

  override o.Equals(ob : obj) =
    match ob with
    | :? triple as other -> other.X = o.X && other.Y = o.Y && other.Z = o.Z
    | _ -> false
  override o.GetHashCode() =
    let hash = 23.
    let hash = hash * 31. + o.X
    let hash = hash * 31. + o.Y
    int (hash * 31. + o.Z)
  (*[/omit]*)

module Triple =
  let toArray (t: triple) = [| t.X; t.Y; t.Z |]
  let toList (t: triple) = [t.X; t.Y; t.Z]
  let toTuple (t: triple) = t.X, t.Y, t.Z
  let dot (a: triple) (b: triple) = a.X * b.X + a.Y * b.Y + a.Z * b.Z
  let outer (a: triple) (b: triple) = 
    array2D [|  [| a.X * b.X; a.X * b.Y; a.X * b.Z |];
                [| a.Y * b.X; a.Y * b.Y; a.Y * b.Z |];
                [| a.Z * b.X; a.Z * b.Y; a.Z * b.Z |] |]
  let norm (t: triple) = sqrt (t.X * t.X + t.Y * t.Y + t.Z * t.Z)
  let norm2 (t: triple) = t.X * t.X + t.Y * t.Y + t.Z * t.Z
  let cross (a: triple) (b: triple) = triple(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X)    
  let map2 (fn: float->float->float) (a: triple) (b: triple) = triple(fn a.X b.X, fn a.Y b.Y, fn a.Z b.Z)
  let Zero = triple(0., 0., 0.)
  let Ones = triple(1.,1.,1.)
  let create (n: float) = triple(n,n,n)
  let exists fn (el: triple) =
    if fn el.X then true
    elif fn el.Y then true
    elif fn el.Z then true
    else false
  let map (fn: float->float) (el: triple) = triple(fn el.X, fn el.Y, fn el.Z)
  let normalize (t: triple) = t / (norm t)
  let init (fn: int->float) = triple(fn 0, fn 1, fn 2)

(* // usage
let tr1 = triple(5,2,3)
let tr2 = triple(5,2,3)
let v1 = (-tr1 + tr2 - tr1) .* tr2 ./ tr1 .** 2. * tr1 + tr1.[0] + tr2.X
let v2 = tr1.norm + Triple.norm2 tr2
// ... etc *)