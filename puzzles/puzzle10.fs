namespace AlgorithmPuzzles

open System

module puzzle10 =
    type Point = { x: int; y: int; value: int }
    type Map = Point array array

    let loadMap(filePath: string): Map = 
        System.IO.File.ReadAllLines(filePath)
        |> Array.mapi (fun row line -> 
            line.ToCharArray()
            |> Array.mapi (fun col c -> 
                { x = row; y = col; value = System.Char.GetNumericValue(c) |> int }))

    let validNextPoint (map: Map, x: int, y: int, value: int): bool =
        x >= 0 && x < map.Length && y >= 0 && y < map.[0].Length && map.[x].[y].value = value + 1 

    let trailHeadScore (map: Map, trailHead: Point): int =
        let mutable score = 0

        let queue = System.Collections.Generic.Queue<Point>()
        let tailSet = System.Collections.Generic.HashSet<Point>()
        queue.Enqueue(trailHead)

        while queue.Count > 0 do
            let point = queue.Dequeue()
            let (x, y, value) = (point.x, point.y, point.value)
            
            if validNextPoint(map, x - 1, y, value) then
                if map.[x - 1].[y].value = 9 then
                    tailSet.Add(map.[x - 1].[y])|> ignore
                    score <- score + 1
                else
                    queue.Enqueue(map.[x - 1].[y])
            if validNextPoint(map, x + 1, y, value) then
                if map.[x + 1].[y].value = 9 then
                    tailSet.Add(map.[x + 1].[y])|> ignore
                    score <- score + 1
                else
                    queue.Enqueue(map.[x + 1].[y])
            if validNextPoint(map, x , y - 1, value) then
                if map.[x].[y - 1].value = 9 then
                    tailSet.Add(map.[x].[y - 1])|> ignore
                    score <- score + 1
                else
                    queue.Enqueue(map.[x].[y - 1])
            if validNextPoint(map, x , y + 1, value) then
                if map.[x].[y + 1].value = 9 then
                    tailSet.Add(map.[x].[y + 1])|> ignore
                    score <- score + 1
                else
                    queue.Enqueue(map.[x].[y + 1])

        tailSet.Count

    let trailHeadRating (map: Map, trailHead: Point): int =
        let mutable rating = 0

        let queue = System.Collections.Generic.Queue<Point>()
        queue.Enqueue(trailHead)

        while queue.Count > 0 do
            let point = queue.Dequeue()
            let (x, y, value) = (point.x, point.y, point.value)
            
            if validNextPoint(map, x - 1, y, value) then
                if map.[x - 1].[y].value = 9 then
                    rating <- rating + 1
                else
                    queue.Enqueue(map.[x - 1].[y])
            if validNextPoint(map, x + 1, y, value) then
                if map.[x + 1].[y].value = 9 then
                    rating <- rating + 1
                else
                    queue.Enqueue(map.[x + 1].[y])
            if validNextPoint(map, x , y - 1, value) then
                if map.[x].[y - 1].value = 9 then
                    rating <- rating + 1
                else
                    queue.Enqueue(map.[x].[y - 1])
            if validNextPoint(map, x , y + 1, value) then
                if map.[x].[y + 1].value = 9 then
                    rating <- rating + 1
                else
                    queue.Enqueue(map.[x].[y + 1])
        rating 

    let findTrailHead (map: Map): Point array =
        map
        |> Array.concat
        |> Array.filter (fun point -> point.value = 0)

    let sumAllTrailHeadScore (map: Map): int =
        findTrailHead(map)
        |> Array.map (fun point -> trailHeadScore(map, point))
        |> Array.sum

    let sumAllTrailHeadRating (map: Map): int =
        findTrailHead(map)
        |> Array.map (fun point -> trailHeadRating(map, point))
        |> Array.sum