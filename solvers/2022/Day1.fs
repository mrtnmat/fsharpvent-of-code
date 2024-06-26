﻿namespace Solvers.Y2022

open System.IO
open System.Text.RegularExpressions

module Day1 =
    module private Local =
        let rec replaceLastElement (list: 'T List) newValue =
            match list with
            | [] -> [ newValue ]
            | [ _ ] -> [ newValue ]
            | head :: tail -> head :: (replaceLastElement tail newValue)

        let parseInput (path: string) =
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

        let solvePart1 (filePath: string) =
            filePath
            |> parseInput
            |> List.map (List.fold (fun s v -> s + v) 0)
            |> List.fold
                (fun s v ->
                    match s, v with
                    | s, v when s >= v -> s
                    | _, v -> v)
                0

        let solvePart2 (filePath: string) =
            filePath
            |> parseInput
            |> List.map (List.fold (fun s v -> s + v) 0)
            |> List.sortDescending
            |> List.take 3
            |> List.fold (fun s v -> s + v) 0

    let solve (filePath: string) =

        (Local.solvePart1 filePath, Local.solvePart2 filePath)
