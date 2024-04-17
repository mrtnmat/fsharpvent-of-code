﻿namespace Solvers.Y2019

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

        let AllPairsFromList (lst: 'a list) =
            let rec f acc l =
                match l with
                | [] -> acc
                | [ _ ] -> acc
                | (x :: xs) -> f ((List.allPairs [ x ] xs) :: acc) xs

            f [] lst |> List.concat

        let AllTriples (inp: int list) : int list seq =
            seq {
                for i in 1 .. List.length inp do
                    for j in i + 1 .. List.length inp do
                        for k in j + 1 .. List.length inp do
                            yield [ i; j; k ]
            }
            |> Seq.map (fun l -> List.map (fun i -> inp.[i - 1]) l)

    let solvePart1 (filePath: string) : int = 0

    let solvePart2 (filePath: string) : int = 0

    let solve (filePath: string) =
        (solvePart1 filePath, solvePart2 filePath)