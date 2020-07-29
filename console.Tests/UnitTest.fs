namespace DownhillSimplex.Tests

open System
open NUnit.Framework
open DownhillSimplex

[<TestFixture>]
type TestClass () =
    
    [<Test>]
    member this.TestMakeSimplex() =
        let initVertex = [1.0; 100.0]
        let expected = [[1.0; 100.0]; [1.1 * 1.0; 100.0]; [1.0; 1.1 * 100.0]]
        let actual = NM.makesimplex initVertex
        Assert.That(actual, Is.EqualTo(expected))
        //CollectionAssert.AreEquivalent(expected, actual)

    [<Test>]
    member this.TestCentroid() =
        let initVertices = [[1.0; 100.0]; [-40.0; 100.0]; [3.0; -20.0]]
        let expected = [-12.0; 60.0]
        let actual = NM.centroid initVertices
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
            [[1.0; 100.0]; [1.1; 100.0]; [1.0; 1.0]; [1.0; 110.0]]
            |> List.map NM.tuple
        let expected = 2, 0.0
        let actual = NM.argMin f vertices
        Assert.That(actual, Is.EqualTo(expected))

    // Objective function
    [<Test>]
    member this.TestBanana() =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        let objFcn = bananaFcn (1.0, 100.0)
        let minl = [1.0; 1.0]
        let mint = NM.tuple minl
        let expected = 0.0
        let actual = NM.objFcn mint
        Assert.That(actual, Is.EqualTo(expected))

    // Main
    [<Test>]
    member this.TestDownhillSimplex() =
        let objFcn = NM.objFcn
        let initVertex = [1.0; 1.0]     
        let expected = ((1.0, 1.0), 0.0)
        let actual = NM.fit objFcn initVertex
        Assert.That(actual, Is.EqualTo(expected))



