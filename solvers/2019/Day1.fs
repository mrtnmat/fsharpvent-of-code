namespace Solvers.Y2019

open System.IO

module Day1 =

    module private Local =

        let parseInput (path: string) =
            use s = new StreamReader(path)

            seq {
                while not s.EndOfStream do
                    yield s.ReadLine()
            }
            |> Seq.map (int)
            |> Seq.toList
        
        let fuelReq (mass: int) =
            mass / 3 - 2
        
        let fuelReq2 (mass: int) =
            let rec f (m: int) (acc: int) =
                match (fuelReq m) with
                | fuel when fuel < 1 -> acc
                | fuel -> f fuel (acc + fuel)
            f mass 0

    let solvePart1 (filePath: string) : int =
        Local.parseInput filePath
        |> List.map(Local.fuelReq)
        |> List.fold(fun s v -> s + v) 0

    let solvePart2 (filePath: string) : int =
        Local.parseInput filePath
        |> List.map(Local.fuelReq2)
        |> List.fold(fun s v -> s + v) 0

    let solve (filePath: string) =
        (solvePart1 filePath, solvePart2 filePath)
