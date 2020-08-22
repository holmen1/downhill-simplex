namespace DownhillSimplex.Tests

open System
open NUnit.Framework
open DownhillSimplex
open FSharp.Numerics


[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.TestBump() =
        let v = Vertex(1.0, 5.0)
        let expected = Vertex(1.0, 5.5)
        let actual = NM.bump 1 (fun x -> 1.1 * x) v
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestMakeSimplex() =
        let initVertex = Vertex(1.0, 100.0)
        let expected = List.map Vertex.toVertex [(1.0, 100.0); (1.1 * 1.0, 100.0); (1.0, 1.1 * 100.0)]
        let actual = NM.makeSimplex initVertex
        Assert.That(actual, Is.EqualTo(expected))
        //CollectionAssert.AreEquivalent(expected, actual)

    [<Test>]
    member this.TestSumSimplex() =
        let simplex =
            List.map Vertex.toVertex [(1.0, 100.0); (-40.0, 100.0)]
        let expected = Vertex.toVertex (-39.0, 200.0)
        let actual = NM.sumSimplex simplex
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestCentroid() =
        let simplex =
            List.map Vertex.toVertex [(1.0, 100.0); (-40.0, 100.0); (3.0, -20.0)]
        let expected = Vertex.toVertex (-12.0, 60.0)
        let actual = NM.centroid simplex
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestReflection() =
        let xh = Vertex.toVertex (0.0, 3.0)
        let xc = Vertex.toVertex (0.0, 1.0)
        let expected = Vertex.toVertex (0.0, - 1.0)
        let actual = NM.reflection xc xh
        Assert.That(actual, Is.EqualTo(expected))
(*
    [<Test>]
    member this.TestRemove() =
        let xs = [0.0; 1.0; 8.0; 3.0]
        let expected = [0.0; 1.0; 3.0]
        let actual = NM.remove 2 xs
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestArgMax() =
        let f = (fun (x, y) -> 4.0 - x ** 2.0 + y ** 2.0)
        let tuples  = [(1.0, 100.0); (1.1, 100.0); (1.0, 110.0)]
        let expected = 2, 12103.0
        let actual = NM.argMax f tuples
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestArgMin() =
        let f = NM.objFcn
        let vertices =
            List.map NM.Vertex [[1.0; 100.0]; [1.1; 100.0]; [1.0; 1.0]; [1.0; 110.0]]
        let expected = 2, 0.0
        let actual = NM.argMin f vertices
        Assert.That(actual, Is.EqualTo(expected))

    // Objective function
    [<Test>]
    member this.TestBanana() =
        let min = [1.0; 1.0] |> NM.Vertex
        let expected = 0.0
        let actual = NM.objFcn min
        Assert.That(actual, Is.EqualTo(expected))

    // Main
    [<Test>]
    member this.TestDownhillSimplex() =
        let objFcn = NM.objFcn
        let initVertex = [1.0; 1.0] |> NM.Vertex  
        let expected = (initVertex, 0.0)
        let actual = NM.fit initVertex
        Assert.That(actual, Is.EqualTo(expected)) *)



