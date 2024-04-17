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

        let AllTriples (inp: int list) : int list seq =
            seq {
                for i in 1 .. List.length inp do
                    for j in i + 1 .. List.length inp do
                        for k in j + 1 .. List.length inp do
                            yield [ i; j; k ]
            }
            |> Seq.map (fun l -> List.map (fun i -> inp.[i - 1]) l)

    let solvePart1 (filePath: string) : int =
        Local.parseInput filePath
        |> Local.AllPairsFromList
        |> List.filter (fun (x, y) -> x + y = 2020)
        |> List.head
        |> (fun (x, y) -> x * y)

    let solvePart2 (filePath: string) : int =
        Local.parseInput filePath
        |> Local.AllTriples
        |> Seq.filter (fun l -> (List.fold (fun s v -> s + v) 0 l) = 2020)
        |> Seq.head
        |> List.fold (fun s t -> s * t) 1

    let solve (filePath: string) =
        (solvePart1 filePath, solvePart2 filePath)
