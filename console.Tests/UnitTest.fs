namespace DownhillSimplex.Tests

open System
open NUnit.Framework
open DownhillSimplex

[<TestFixture>]
type TestClass () =
    
    [<Test>]
    member this.TestInitializeVertices() =
        let initVertex = [1.0; 100.0]
        let expected = [[1.0; 100.0]; [1.1 * 1.0; 100.0]; [1.0; 1.1 * 100.0]]
        let actual = NM.initializeVertices initVertex
        Assert.That(actual, Is.EqualTo(expected))
        //CollectionAssert.AreEquivalent(expected, actual)

    [<Test>]
    member this.TestCentroid() =
        let initVertices = [[1.0; 100.0]; [-40.0; 100.0]; [3.0; -20.0]]
        let expected = [-12.0; 60.0]
        let actual = NM.centroid initVertices
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestEvaluateVertices() =
        let initVertices = [[1.0; 100.0]; [1.1 * 1.0; 100.0]; [1.0; 1.1 * 100.0]]
        let values = [1.0 + 100.0; 1.1 * 1.0 + 100.0; 1.0 + 1.1 * 100.0]
        let expected = List.zip values initVertices
        let actual = NM.evaluateVertices (fun [x:float; y:float] -> x + y) initVertices
        Assert.That(actual, Is.EqualTo(expected))

    [<Test>]
    member this.TestArgMax() =
        let f = (fun [x; y] -> 4.0 - x ** 2.0 + y ** 2.0)
        let vertices = [[1.0; 100.0]; [1.1; 100.0]; [1.0; 110.0]]
        let expected = 2, 12103.0
        let actual = NM.argMax f vertices
        Assert.That(actual, Is.EqualTo(expected))

    // Sort
    [<Test>]
    member this.TestOrderingVertices() =
        let t0 = 0.23, (0.0, 9.99)
        let t1 = 1.23, (0.0, 9.99)
        let t2 = 2.23, (0.0, 9.99)
        let t3 = 3.23, (0.0, 9.99)
        let valueList = [t2; t3; t0; t1]
        let expected = [t0; t1; t2; t3]
        let actual = NM.orderVertices valueList
        Assert.That(actual, Is.EqualTo(expected))

    // Objective function
    [<Test>]
    member this.TestBanana() =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        let objFcn = bananaFcn (1.0, 100.0)
        let minl = [1.0; 1.0]
        let mint = NM.toTuple2 minl
        let expected = 0.0
        let actual = NM.objFcn mint
        Assert.That(actual, Is.EqualTo(expected))

    // Main
    [<Test>]
    member this.TestDownhillSimplex() =
        let bananaFcn ((a,b): float*float) ((x,y): float*float) =
            (a - x) ** 2.0 + b * (y - x ** 2.0) ** 2.0
        let objFcn = bananaFcn (1.0, 100.0)
        let init = (-0.5, 3.0)
        let expected = (1.0, 1.0)
        let actual = NM.fit objFcn expected //init
        Assert.That(actual, Is.EqualTo(expected))



