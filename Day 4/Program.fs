open System.IO

let fileName = "input.txt"
let lines = File.ReadAllLines fileName
let matrix = Array2D.init lines.Length lines.Length (fun i j -> lines[i][j])

// Part 1
let countXmas (i: int) (j: int) (matrix: char array2d) =
    let mutable count = 0

    if matrix[i, j] = 'X' then
        // Up
        if i >= 3 then
            if matrix[i - 1, j] = 'M' && matrix[i - 2, j] = 'A' && matrix[i - 3, j] = 'S' then
                count <- count + 1

        // Up right
        if i >= 3 && j < (matrix.GetLength(1) - 3) then
            if
                matrix[i - 1, j + 1] = 'M'
                && matrix[i - 2, j + 2] = 'A'
                && matrix[i - 3, j + 3] = 'S'
            then
                count <- count + 1

        // Right
        if j < (matrix.GetLength(1) - 3) then
            if matrix[i, j + 1] = 'M' && matrix[i, j + 2] = 'A' && matrix[i, j + 3] = 'S' then
                count <- count + 1

        // Down right
        if i < (matrix.GetLength(0) - 3) && j < (matrix.GetLength(1) - 3) then
            if
                matrix[i + 1, j + 1] = 'M'
                && matrix[i + 2, j + 2] = 'A'
                && matrix[i + 3, j + 3] = 'S'
            then
                count <- count + 1

        // Down
        if i < (matrix.GetLength(0) - 3) then
            if matrix[i + 1, j] = 'M' && matrix[i + 2, j] = 'A' && matrix[i + 3, j] = 'S' then
                count <- count + 1

        // Down left
        if i < (matrix.GetLength(0) - 3) && j >= 3 then
            if
                matrix[i + 1, j - 1] = 'M'
                && matrix[i + 2, j - 2] = 'A'
                && matrix[i + 3, j - 3] = 'S'
            then
                count <- count + 1

        // Left
        if j >= 3 then
            if matrix[i, j - 1] = 'M' && matrix[i, j - 2] = 'A' && matrix[i, j - 3] = 'S' then
                count <- count + 1

        // Up left
        if i >= 3 && j >= 3 then
            if
                matrix[i - 1, j - 1] = 'M'
                && matrix[i - 2, j - 2] = 'A'
                && matrix[i - 3, j - 3] = 'S'
            then
                count <- count + 1

    count


let mutable count = 0

for i = 0 to Array2D.length1 matrix - 1 do
    for j = 0 to Array2D.length2 matrix - 1 do
        count <- count + (countXmas i j matrix)

printfn "%i" count

// Part 2
let countCrossingMas (i: int) (j: int) (matrix: char array2d) =
    let mutable count = 0

    if
        i > 0
        && j > 0
        && j < (matrix.GetLength(1) - 1)
        && i < (matrix.GetLength(0) - 1)
        && matrix[i, j] = 'A'
    then
        if
            (matrix[i - 1, j + 1] = 'S'         // Top right
             && matrix[i + 1, j - 1] = 'M'      // Bottom left
             && matrix[i - 1, j - 1] = 'S'      // Top left
             && matrix[i + 1, j + 1] = 'M')     // Bottom right

            || (matrix[i - 1, j + 1] = 'M'      // Top right
                && matrix[i + 1, j - 1] = 'S'   // Bottom left
                && matrix[i - 1, j - 1] = 'M'   // Top left
                && matrix[i + 1, j + 1] = 'S')  // Bottom right

            || (matrix[i - 1, j + 1] = 'M'      // Top right
                && matrix[i + 1, j - 1] = 'S'   // Bottom left
                && matrix[i - 1, j - 1] = 'S'   // Top left
                && matrix[i + 1, j + 1] = 'M')  // Bottom right

            || (matrix[i - 1, j + 1] = 'S'      // Top right
                && matrix[i + 1, j - 1] = 'M'   // Bottom left
                && matrix[i - 1, j - 1] = 'M'   // Top left
                && matrix[i + 1, j + 1] = 'S')  // Bottom right

        then
            count <- count + 1

    count

count <- 0

for i = 0 to Array2D.length1 matrix - 1 do
    for j = 0 to Array2D.length2 matrix - 1 do
        count <- count + (countCrossingMas i j matrix)

printfn "%i" count
