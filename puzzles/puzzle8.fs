namespace AlgorithmPuzzles

open System

module puzzle8 =
    type Position = int * int
    type Antinode = Position
    type Antenna = Char * Position
    type Map = char array array

    let parseInput (filePath: string) : Map =
        let lines = System.IO.File.ReadAllLines(filePath)
        lines |> Array.map (fun x -> x.ToCharArray())

    let ConvertMap (m: Map) : Antenna array =
        [ for i in 0 .. m.Length - 1 do
              for j in 0 .. m.[i].Length - 1 do
                  let c = m.[i].[j]

                  if c <> '.' then
                      yield (c, (i, j)) ]
        |> List.toArray

    let findAntennaPair (aa: Antenna array) : (Antenna * Antenna) array =
        [ for i in 0 .. aa.Length - 1 do
              for j in i + 1 .. aa.Length - 1 do
                  if fst aa.[i] = fst aa.[j] then
                      yield (aa.[i], aa.[j]) ]
        |> List.toArray

    let populateAntinode (antennaPair: (Antenna * Antenna)) : Antinode array =
        let (ant1, pos1) = fst antennaPair
        let (ant2, pos2) = snd antennaPair
        let (x1, y1) = pos1
        let (x2, y2) = pos2

        let a1 = (x1 - (x2 - x1), y1 - (y2 - y1))
        let a2 = (x2 + (x2 - x1), y2 + (y2 - y1))

        [| a1; a2 |]

    let populateAntinode2 (m: Map) (antennaPair: (Antenna * Antenna)) : Antinode array =
        let (ant1, pos1) = fst antennaPair
        let (ant2, pos2) = snd antennaPair
        let (x1, y1) = pos1
        let (x2, y2) = pos2

        // generate all possible antinode that in the same line with the antenna pair but within the map
        let disX = x2 - x1
        let disY = y2 - y1

        let rec generateAntinode (x: int) (y: int) (acc: Antinode list) =
            if x < 0 || y < 0 || x >= m.Length || y >= m.[0].Length then
                acc
            else
                generateAntinode (x + disX) (y + disY) ((x, y) :: acc)

        let rec generateAntinode2 (x: int) (y: int) (acc: Antinode list) =
            if x < 0 || y < 0 || x >= m.Length || y >= m.[0].Length then
                acc
            else
                generateAntinode2 (x - disX) (y - disY) ((x, y) :: acc)

        let a1 = generateAntinode x2 y2 []
        let a2 = generateAntinode2 x1 y1 []

        a1 @ a2 |> List.toArray







    let findAntinode (m: Map) : Antinode array =
        let aa = ConvertMap m
        let pairs = findAntennaPair aa

        pairs
        |> Array.collect populateAntinode
        |> Array.distinct
        |> Array.filter (fun x -> fst x >= 0 && snd x >= 0 && fst x < m.Length && snd x < m.[0].Length)

    let findAntinode2 (m: Map) : Antinode array =
        let aa = ConvertMap m
        let pairs = findAntennaPair aa

        pairs
        |> Array.collect (populateAntinode2 m)
        |> Array.distinct
        |> Array.filter (fun x -> fst x >= 0 && snd x >= 0 && fst x < m.Length && snd x < m.[0].Length)
