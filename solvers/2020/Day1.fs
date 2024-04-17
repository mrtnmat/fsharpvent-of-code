namespace Solvers.Y2020

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

    let solvePart1 (filePath: string) : int =
        Local.parseInput filePath
        |> Local.AllPairsFromList
        |> List.filter (fun (x, y) -> x + y = 2020)
        |> List.head
        |> (fun (x, y) -> x * y)

    let solvePart2 (filePath: string) : int = 0

    let solve (filePath: string) =

        (solvePart1 filePath, solvePart2 filePath)
