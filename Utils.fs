module Utils

open System.IO

let readFile (filePath: string) : Option<string> =
    try
        use reader = new StreamReader(filePath)
        let contents = reader.ReadToEnd()
        Some contents
    with _ ->
        None

let readFileLines (filePath: string) : Option<string seq> =
    try
        use reader = new StreamReader(filePath)

        let lines =
            seq {
                while not reader.EndOfStream do
                    yield reader.ReadLine()
            }

        Some lines
    with _ ->
        None
