namespace Solvers

open System.IO
open System.Text.RegularExpressions

module Y2021 =
    module Day1 =
        module private Local =

            let parseInput (path: string) =
                use s = new StreamReader(path)

                seq {
                    while not s.EndOfStream do
                        yield s.ReadLine()
                }
                |> Seq.map(int)
                |> Seq.toList

            let solveN (filePath: string) (n: int): int =
                filePath
                |> parseInput
                |> (fun s -> Seq.map2 (fun n1 n2 -> n1 - n2) (Seq.skip n s) s)
                |> Seq.filter (fun n -> n > 0)
                |> Seq.length

            let solvePart1 (filePath: string): int =
                solveN filePath 1

            let solvePart2 (filePath: string): int =
                solveN filePath 3

        let solve (filePath: string) =

            (Local.solvePart1 filePath, Local.solvePart2 filePath)
