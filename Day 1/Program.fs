open System
open System.Collections.Generic
open System.IO

let fileName = "input.txt"

let (leftList, rightList) =
    File.ReadAllLines fileName
    |> Seq.fold
        (fun (leftList, rightList) line ->
            match line.Split("   ") with
            | [| left; right |] -> leftList @ [ int left ], rightList @ [ int right ]
            | _ -> failwith "No left nor right :(")
        (List.empty, List.empty)

// Part 1
Seq.zip (leftList |> List.sort) (rightList |> List.sort)
|> Seq.sumBy (fun (left, right) -> Math.Abs(left - right))
|> printfn "%i"


// Part 2
let memoize (dict: IDictionary<_, _>) fn x =
    match dict.TryGetValue x with
    | true, v -> v
    | false, _ ->
        let v = fn (x)
        dict.Add(x, v)
        v

let findInstances =
    memoize (Dictionary<int, int>()) (fun l ->
        rightList
        |> Seq.fold (fun instances r -> if l = r then instances + 1 else instances) 0)

leftList
|> Seq.sumBy (fun left ->
    let instances = findInstances left
    left * instances)
|> printfn "%i"
