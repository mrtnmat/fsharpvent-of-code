module Program

open System
open System.IO
open System.Text.RegularExpressions

let wordNumbers =
    [| "zero"
       "one"
       "two"
       "three"
       "four"
       "five"
       "six"
       "seven"
       "eight"
       "nine" |]
let parseWordNumber (line: string): string =
    match Array.tryFindIndex ((=) line) wordNumbers with
    | Some i -> sprintf "%A" i
    | None -> line
    
let parseLine (line: string) : int =
    seq [| "(\d)"; ".*(\d).*" |]
    |> Seq.map Regex
    |> Seq.map (fun (r: Regex) -> r.Match(line).Groups.Item(1).Value)
    |> String.concat ""
    |> int

let parseLine2 (line: string) : int =
    [| sprintf "(\d|%s).*" (String.concat "|" wordNumbers)
       sprintf ".*(\d|%s)" (String.concat "|" wordNumbers) |]
    |> Seq.map Regex
    |> Seq.map (fun (r: Regex) -> r.Match(line).Groups.Item(1).Value)
    |> Seq.map parseWordNumber
    |> String.concat ""
    |> int

[<EntryPoint>]
let main argv =
    let filePath = Path.Combine(Environment.CurrentDirectory, "input", "2023", "day1")
    use reader = new StreamReader(filePath)

    seq {
        while not reader.EndOfStream do
            yield reader.ReadLine()
    }
    |> Seq.map (fun s -> [ parseLine s; parseLine2 s ])
    |> Seq.fold (fun acc v -> List.map2 (fun a b -> a + b) v acc) [ 0; 0 ]
    |> printfn "result: %A"

    0
