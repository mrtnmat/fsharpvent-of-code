namespace Solvers

open System.IO
open System.Text.RegularExpressions

module Y2022 =
    module Day1 =
        module private Local =
            let rec replaceLastElement (list: 'T List) newValue =
                match list with
                | [] -> [ newValue ]
                | [ x ] -> [ newValue ]
                | head :: tail -> head :: (replaceLastElement tail newValue)

            let parseInput (filePath: string) : string list list =
                use s = new StreamReader(filePath)

                let res () =
                    let rec r =
                        fun (output: string list list) (line: string) ->
                            let c2 = fun (l: string) -> Regex("\d+").Match(l)

                            let c3 =
                                fun (m: Match) ->
                                    match m.Success with
                                    | true -> Some m.Value
                                    | false -> None

                            let c4 =
                                fun (r: string Option) ->
                                    match r with
                                    | Some a -> replaceLastElement output (a :: List.last output)
                                    | None -> output @ [ [] ]

                            let c =
                                fun out ->
                                    match s.ReadLine() with
                                    | null -> out
                                    | line -> r out line

                            line |> c2 |> c3 |> c4 |> c

                    r [ [] ] (s.ReadLine())

                res ()

            let parseInputNew (path: string) =
                use s = new StreamReader(path)

                seq {
                    while not s.EndOfStream do
                        yield s.ReadLine()
                }
                |> Seq.fold
                    (fun acc v ->
                        match Regex("\d+").Match(v).Success with
                        | true -> replaceLastElement acc (int v :: List.last acc)
                        | false -> acc @ [ [] ])
                    [ [] ]


        let solve (filePath: string) =

            let r = Regex "((?:\d+\n)+)"

            List.iter (printfn "%A") (Local.parseInput filePath)
            List.iter (printfn "%A") (Local.parseInputNew filePath)

            (0, 0)

//|> Seq.map (fun s -> (Local.parseLine s, Local.parseLine2 s))
//|> Seq.fold (fun ((a, b): int * int) (a1, b1) -> (a + a1, b + b1)) (0, 0)
