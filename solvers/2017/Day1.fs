namespace Solvers.Y2017

open System.IO

module Day1 =

    module private Local =

        let parseInput (path: string) =
            use s = new StreamReader(path)

            seq {
                while not s.EndOfStream do
                    yield s.Read() |> (fun c -> c - int ('0'))
            }
            |> Seq.filter (fun n -> n >= 0 && n <= 9)
            |> Seq.toList

        let rec loopList (lst: 'a list) =
            seq {
                for item in lst do
                    yield item

                yield! loopList lst
            }

    let solvePart1 (filePath: string) : int =
        let input = Local.parseInput filePath

        input
        |> Seq.map2
            (fun v1 v2 ->
                match v1 = v2 with
                | true -> v1
                | false -> 0)
            (Local.loopList input |> Seq.skip 1)
        |> Seq.reduce (fun a v -> a + v)


    let solvePart2 (filePath: string) : int =
        let input = Local.parseInput filePath

        input
        |> Seq.map2
            (fun v1 v2 ->
                match v1 = v2 with
                | true -> v1
                | false -> 0)
            (Local.loopList input |> Seq.skip (List.length input / 2))
        |> Seq.reduce (fun a v -> a + v)

    let solve (filePath: string) =
        (solvePart1 filePath, solvePart2 filePath)
