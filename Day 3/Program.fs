open System.IO
open System.Text.RegularExpressions

let fileName = "input.txt"

// Part 1
File.ReadAllLines fileName
|> Seq.sumBy (fun line ->
    Regex.Matches(line, "mul\\((\\d{1,3}),(\\d{1,3})\\)")
    |> Seq.sumBy (fun m ->
        let left = int m.Groups[1].Value
        let right = int m.Groups[2].Value
        left * right))
|> printfn "%i"

// Part 2
let (sum, _) =
    Regex.Matches(File.ReadAllText fileName, "do\\(\\)|don't\\(\\)|mul\\((\\d{1,3}),(\\d{1,3})\\)")
    |> Seq.fold
        (fun ((sum, doExecute)) m ->
            match m.Value with
            | "do()" -> (sum, true)
            | "don't()" -> (sum, false)
            | _ ->
                match doExecute with
                | false -> (sum, false)
                | _ ->
                    let left = int m.Groups[1].Value
                    let right = int m.Groups[2].Value
                    (sum + (left * right), doExecute))
        (0, true)

printfn "%i" sum
