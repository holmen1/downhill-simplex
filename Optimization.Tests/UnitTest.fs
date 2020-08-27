namespace DownhillSimplex.Tests

open NUnit.Framework
open DownhillSimplex
open FSharp.Numerics
open FSharp.Numerics.Vertex


[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.TestBump() =
        let v = Vertex(1.0, 5.0)
        let expected = Vertex(1.0, 5.5)
        let actual = bump 1 (fun x -> 1.1 * x) v
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestMakeSimplex() =
        let initVertex = Vertex(1.0, 100.0)
        let expected = List.map Vertex.toVertex [(1.0, 100.0); (1.2 * 1.0, 100.0); (1.0, 1.2 * 100.0)]
        let actual = makeSimplex initVertex
        Assert.That(actual, Is.EqualTo(expected))
        //CollectionAssert.AreEquivalent(expected, actual)

    [<Test>]
    member this.TestCentroid() =
        let simplex =
            List.map Vertex.toVertex [(1.0, 100.0); (-40.0, 100.0); (3.0, -20.0)]
        let expected = Vertex.toVertex (-12.0, 60.0)
        let actual = centroid simplex
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestReflection() =
        let xh = Vertex.toVertex (0.0, 3.0)
        let xc = Vertex.toVertex (0.0, 1.0)
        let expected = Vertex.toVertex (0.0, - 1.0)
        let actual = reflection xc xh
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestRemove() =
        let xs = [0.0; 1.0; 8.0; 3.0]
        let expected = [0.0; 1.0; 3.0]
        let actual = remove 2 xs
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestArgMax() =
        let f = NM.objFcn
        let simplex =
            List.map Vertex.toVertex [(2.0, 2.0); (1.1*2.0, 2.0); (2.0, 1.1*2.0)]
        let expected = 1, 808.00000000000045
        let actual = argMax f simplex
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestArgMin() =
        let f = NM.objFcn
        let simplex =
            List.map Vertex.toVertex [(1.0, 1.1); (1.1, 1.0); (1.0, 1.0); (1.1, 1.1)]
        let expected = 2, 0.0
        let actual = argMin f simplex
        Assert.That(actual, Is.EqualTo(expected))

    // Main
    [<Test>]
    member this.TestDownhillSimplex() =
        let objFcn = NM.objFcn
        let initVertex = Vertex(3.0, 5.5)
        let expected = (1.0, 1.0)
        let actual, _, _, _= NM.fit initVertex
        Assert.That(actual, Is.EqualTo(expected))
    // Expected: (1.0d, 1.0d)
    // But was:  (0.99999999995723488d, 0.99999999991435362d)



