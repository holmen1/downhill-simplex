namespace DownhillSimplex.FSharp

open System

type OptimalDiscountFactors
    (
        p1: float,
        p2: float,
        p3: float,
        p4: float,
        p5: float,
        p6: float,
        p7: float,
        p8: float,
        p9: float,
        p10: float,
        p12: float,
        p15: float,
        p20: float
    ) =
    inherit
        DownhillSimplex(
            Vertex
                [ 1.0
                  1.0
                  1.0
                  1.0
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9
                  0.9 ]
        )


    let floor = 0.0
    let creditrisk = 0.0035
    let T1 = 10
    let T2 = 20
    let UFR = 0.042

    let adjustPar (par: float) (creditrisk: float) (golv: float) =
        System.Math.Max(golv, par / 100.0 - creditrisk)

    let par' =
        [ 99.9
          p1
          p2
          p3
          p4
          p5
          p6
          p7
          p8
          p9
          p10
          99.9
          p12
          99.9
          99.9
          p15
          99.9
          99.9
          99.9
          99.9
          p20 ]
        |> List.map (fun p -> adjustPar p creditrisk floor)

    let DF = 1.0 :: (base.fit base.initGuess)
    let DF' = List.map2 (fun p df -> if p = 0.0 then 1.0 else df) par' DF

    let weight (dur: int) =
        if dur <= T1 then 0.0
        elif dur <= T2 then float (dur - T1) / float (T2 - T1 + 1)
        else 1.0

    let zeroCouponRate (dur: int) = DF'.[dur] ** (-1.0 / (float dur)) - 1.0

    let forwardRate (dur: int) =
        if dur <= T2 then DF'.[dur - 1] / DF'.[dur] - 1.0 else UFR

    let weightedForwardRate (dur: int) =
        (1.0 - weight dur) * forwardRate dur + (weight dur) * UFR

    member this.discountFactor(dur: int) =
        if dur < T2 then DF'.[dur] else DF'.[T2]

    member this.fiRate(dur: int) =
        let rec fi dur =
            match dur with
            | 0
            | 1 -> zeroCouponRate 1
            | dur ->
                ((1.0 + fi (dur - 1)) ** (float dur - 1.0) * (1.0 + weightedForwardRate dur))
                ** (1.0 / float dur)
                - 1.0

        fi dur

    override this.tol = 1E-8

    override this.objective(Vertex v) =
        let DF = 1.0 :: v // making DF 1-indexed

        if not (List.forall (fun df -> df > 0.0) DF.[1..20]) then
            100.0
        else
            (par'.[1] * DF.[1] + DF.[1] - 1.0) ** 2.0
            + (par'.[2] * (List.sum DF.[1..2]) + DF.[2] - 1.0) ** 2.0
            + (par'.[3] * (List.sum DF.[1..3]) + DF.[3] - 1.0) ** 2.0
            + (par'.[4] * (List.sum DF.[1..4]) + DF.[4] - 1.0) ** 2.0
            + (par'.[5] * (List.sum DF.[1..5]) + DF.[5] - 1.0) ** 2.0
            + (par'.[6] * (List.sum DF.[1..6]) + DF.[6] - 1.0) ** 2.0
            + (par'.[7] * (List.sum DF.[1..7]) + DF.[7] - 1.0) ** 2.0
            + (par'.[8] * (List.sum DF.[1..8]) + DF.[8] - 1.0) ** 2.0
            + (par'.[9] * (List.sum DF.[1..9]) + DF.[9] - 1.0) ** 2.0
            + (par'.[10] * (List.sum DF.[1..10]) + DF.[10] - 1.0) ** 2.0
            +
            // log-linear interpolation for maturity 11 years
            (DF.[11] - exp ((log (DF.[10]) + log (DF.[12])) / 2.0)) ** 2.0
            + (par'.[12] * (List.sum DF.[1..12]) + DF.[12] - 1.0) ** 2.0
            +
            // log-linear interpolation for maturities 13, 14 years
            (DF.[13] - exp (log (DF.[12]) * 2.0 / 3.0 + log (DF.[15]) / 3.0)) ** 2.0
            + (DF.[14] - exp (log (DF.[12]) / 3.0 + log (DF.[15]) * 2.0 / 3.0)) ** 2.0
            + (par'.[15] * (List.sum DF.[1..15]) + DF.[15] - 1.0) ** 2.0
            +
            // log-linear interpolation for maturities 16, 17, 18 and 19 years
            (DF.[16] - exp (log (DF.[15]) * 4.0 / 5.0 + log (DF.[20]) / 5.0)) ** 2.0
            + (DF.[17] - exp (log (DF.[15]) * 3.0 / 5.0 + log (DF.[20]) * 2.0 / 5.0)) ** 2.0
            + (DF.[18] - exp (log (DF.[15]) * 2.0 / 5.0 + log (DF.[20]) * 3.0 / 5.0)) ** 2.0
            + (DF.[19] - exp (log (DF.[15]) / 5.0 + log (DF.[20]) * 4.0 / 5.0)) ** 2.0
            + (par'.[20] * (List.sum DF.[1..20]) + DF.[20] - 1.0) ** 2.0
