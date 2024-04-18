namespace Solvers.Y2018

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

        let rec loopList (lst: 'a list) =
            seq {
                for item in lst do
                    yield item

                yield! loopList lst
            }

    let solvePart1 (filePath: string) : int =
        Local.parseInput filePath |> List.fold (fun s v -> s + v) 0

    let solvePart2 (filePath: string) : int =
        Local.parseInput filePath
        |> Local.loopList
        |> Seq.scan
            (fun (acc, visited) vl ->
                match visited with
                | None -> (vl, None)
                | Some set ->
                    match acc + vl with
                    | x when Set.contains x set -> (x, None)
                    | x -> (x, Some(Set.add (x) set)))
            (0, Some Set.empty<int>)
        |> Seq.skipWhile (fun (_, visited) ->
            match visited with
            | None -> false
            | Some _ -> true)
        |> Seq.head
        |> fst

    let solve (filePath: string) =
        (solvePart1 filePath, solvePart2 filePath)
