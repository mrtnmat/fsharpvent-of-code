module Program

open System
open System.IO
open System.Text.RegularExpressions

open Solvers

type Solver = string -> int * int

let solvers: Map<int * int, Solver> =
    Map.empty
    |> Map.add (23, 1) Y2023.Day1.solve
    |> Map.add (22, 1) Y2022.Day1.solve
    |> Map.add (21, 1) Y2021.Day1.solve

[<EntryPoint>]
let main argv =

    let rec readInput () =
        printfn "Insert puzzle to solve"

        Console.ReadLine()
        |> fun (line: string) ->
            let r = "(\d{1,2})(?:\s|\-|,)+[^\d]*(\d{1,2})" |> Regex |> (fun r -> r.Match(line))

            match r.Success with
            | true -> Ok r.Groups
            | false -> Error "Invalid input"
        |> Result.map (fun (m: GroupCollection) -> int m.[1].Value, int m.[2].Value)
        |> Result.bind (fun (key: int * int) ->
            match Map.containsKey key solvers with
            | true -> Ok key
            | false -> Error(sprintf "No solver for this puzzle (Year: 20%A, Day: %A)" <|| key))
        |> Result.bind (fun ((year, day): int * int) ->
            let p =
                Path.Combine(Environment.CurrentDirectory, "input", sprintf "20%A" year, sprintf "day%A" day)

            match File.Exists(p) with
            | true -> Ok(year, day)
            | false -> Error "No input file for this year/day")
        |> fun (a: Result<int * int, string>) ->
            match a with
            | Ok a -> a
            | Error e ->
                printfn "%s" e
                readInput ()



    let year, day = readInput ()

    let path =
        Path.Combine
            [| Environment.CurrentDirectory
               "input"
               sprintf "20%A" year
               sprintf "day%A" day |]

    let sol1, sol2 = (Map.find (year, day) solvers) path

    printfn "year 20%A - day %A" year day
    printfn "[part1: %A, part2: %A]" sol1 sol2

    let _ = Console.ReadKey()

    0
