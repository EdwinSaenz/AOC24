open System
open System.IO

type Sort =
    | Unset = 0
    | Ascending = 1
    | Descending = 2

let getSort (level: int) (nextLevel: int) =
    if level < nextLevel then Sort.Ascending
    elif level > nextLevel then Sort.Descending
    else Sort.Unset

let removeAtIndex (list: int list) (index: int) =
    List.concat [ list.[0 .. index - 1]; list.[index + 1 ..] ]

let isReportSafe report =
    let mutable reportSort = Sort.Unset

    let result =
        report
        |> Seq.pairwise
        |> Seq.tryPick (fun (level: int, nextLevel: int) ->
            let levelsSort = getSort level nextLevel

            if reportSort = Sort.Unset then
                reportSort <- levelsSort

            let difference = Math.Abs(level - nextLevel)

            if reportSort = Sort.Unset then Some()
            elif reportSort <> levelsSort then Some()
            elif difference < 1 || difference > 3 then Some()
            else None)

    match result with
    | None -> true
    | _ -> false

let fileName = "input.txt"
let lines = File.ReadAllLines fileName

let allReports =
    lines |> Seq.map (fun line -> line.Split(" ") |> Seq.map int |> Seq.toList)


// Part 1
allReports
|> Seq.filter (fun report -> isReportSafe report)
|> Seq.length
|> printfn "%i"

// Part 2
allReports
|> Seq.filter (fun report ->
    if isReportSafe report then
        true
    else
        seq { 0 .. report.Length - 1 }
        |> Seq.exists (fun index -> removeAtIndex report index |> isReportSafe))
|> Seq.length
|> printfn "%i"
