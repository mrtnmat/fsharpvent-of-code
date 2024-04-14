namespace Solvers

open System.IO
open System.Text.RegularExpressions

module Y2023 =
    module Day1 =
        module private Local =
            let wordNumbers =
                [ "zero"
                  "one"
                  "two"
                  "three"
                  "four"
                  "five"
                  "six"
                  "seven"
                  "eight"
                  "nine" ]

            let parseWordNumber (line: string) : string =
                match List.tryFindIndex ((=) line) wordNumbers with
                | Some i -> sprintf "%A" i
                | None -> line

            let parseLine (line: string) : int =
                [ "(\d)"; ".*(\d).*" ]
                |> List.map (fun s -> Regex s |> (fun (r: Regex) -> r.Match(line).Groups.Item(1).Value))
                |> String.concat ""
                |> int

            let parseLine2 (line: string) : int =
                [ sprintf "(\d|%s).*" (String.concat "|" wordNumbers)
                  sprintf ".*(\d|%s)" (String.concat "|" wordNumbers) ]
                |> List.map (fun s ->
                    Regex s
                    |> (fun (r: Regex) -> r.Match(line).Groups.Item(1).Value)
                    |> parseWordNumber)
                |> String.concat ""
                |> int

        let solve (filePath: string) : int * int =
            use reader = new StreamReader(filePath)

            seq {
                while not reader.EndOfStream do
                    yield reader.ReadLine()
            }
            |> Seq.map (fun s -> (Local.parseLine s, Local.parseLine2 s))
            |> Seq.fold (fun ((a, b): int * int) (a1, b1) -> (a + a1, b + b1)) (0, 0)
