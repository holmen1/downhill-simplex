type Vertex(x: float list) =
  member this.X = x
  
  static member (+) (u: Vertex, v: Vertex) = Vertex(List.map2 (+) u.X v.X)


let u = Vertex[1.0; 2.0; 3.0]
let v = Vertex[4.0; 5.0; 6.0]


