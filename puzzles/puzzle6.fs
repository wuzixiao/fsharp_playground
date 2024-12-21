namespace AlgorithmPuzzles

module puzzle6 =
    type lab_map = char array array
    type position = (int * int)


    type direction =
        | Right // '>'
        | Left // '<'
        | Up // '^'
        | Down // 'v'
        | Unknown

    type guard = position * direction
    type step = position * direction
    type lab_status = lab_map * guard

    let charToDirection c =
        match c with
        | '>' -> Right
        | '<' -> Left
        | 'v' -> Down
        | '^' -> Up
        | _ -> Unknown



    let parseInput (filePath: string) : lab_map =
        // Read all lines from the file
        let lines_str = System.IO.File.ReadAllLines(filePath)
        lines_str |> Array.map (fun s -> s |> Seq.toArray)

    let initGuard (map: lab_map) : guard =
        let mutable guardFound = None

        for i in 0 .. map.[0].Length - 1 do
            for j in 0 .. map.Length - 1 do
                match map.[i].[j] with
                | '>'
                | '<'
                | '^'
                | 'v' -> guardFound <- Some((i, j), charToDirection map.[i].[j])
                | _ -> ()

        match guardFound with
        | Some g -> g
        | None -> failwith "No guard found"

    let canMove (map: lab_map) (g: guard) : bool =
        if
            (snd g = direction.Down && fst (fst g) = map.Length - 1)
            || (snd g = direction.Up && fst (fst g) = 0)
            || (snd g = direction.Left && snd (fst g) = 0)
            || (snd g = direction.Right && snd (fst g) = map.[0].Length - 1)
        then
            false
        else
            true

    let canMove2 (map: lab_map) (g: guard) : bool =
        let ((x, y), dir) = g

        match dir with
        | direction.Down -> x < map.Length - 1
        | direction.Up -> x > 0
        | direction.Left -> y > 0
        | direction.Right -> y < map.[0].Length - 1
        | direction.Unknown -> failwith "Not Implemented"

    let nextPosChar (g: guard) (map: lab_map) : char =
        let ((x, y), dir) = g

        match dir with
        | direction.Down -> map.[x + 1].[y]
        | direction.Up -> map.[x - 1].[y]
        | direction.Left -> map.[x].[y - 1]
        | direction.Right -> map.[x].[y + 1]
        | direction.Unknown -> failwith "Not Implemented"

    // let turnGuard (map:lab_map) (g: guard) : lab_status =
    let turn (curDirection: direction) : direction =
        match curDirection with
        | Down -> Left
        | Right -> Down
        | Left -> Up
        | Up -> Right
        | Unknown -> failwith "Not Implemented"

    let moveForward (g: guard) : guard =
        let ((x, y), dir) = g

        match dir with
        | Down -> ((x + 1, y), dir)
        | Up -> ((x - 1, y), dir)
        | Left -> ((x, y - 1), dir)
        | Right -> ((x, y + 1), dir)
        | Unknown -> failwith "Not Implemented"

    let nextStatus (map: lab_map) (g: guard) : (lab_status * bool) =
        let ((x, y), dir) = g

        // map.[x].[y] <- 'X'

        if canMove map g then
            let ((x, y), dir) = g

            if nextPosChar g map = '#' then
                let newDirection = turn dir
                ((map, ((x, y), newDirection)), false)
            else
                ((map, moveForward g), true)
        else
            ((map, g), false)

    let IsCompleted (status: lab_status) : bool =
        let (map, g) = status
        let ((x, y), dir) = g

        if canMove2 map g then false else true


    // move guard recursively and return the number of steps
    let rec moveGuard (map: lab_map) (g: guard) (steps: step array) : step array =
        let res = nextStatus map g
        let (status, moved) = res
        let (p, d) = snd status

        if IsCompleted status then
            Array.append steps [| (p, d) |]
        else if moved then
            moveGuard (fst status) (snd status) (Array.append steps [| (snd status) |])
        else
            moveGuard (fst status) (snd status) steps

    // let isWorkingObstable (obstable: obstable) (status: lab_status) : bool =
    //     let (map, g) = status
    //     let ((x, y), dir) = g

    //     let steps = moveGuard map g [| g |]

    //     true

    let newStep (obstacle: position) (s: step) : step =
        let ((x, y), dir) = s
        let (ox, oy) = obstacle

        match dir with
        | Down ->
            if (x + 1 = ox && y = oy) then
                ((x, y - 1), turn dir)
            else
                ((-100, -100), dir)
        | Up ->
            if (x - 1 = ox && y = oy) then
                ((x, y + 1), turn dir)
            else
                ((-100, -100), dir)
        | Left ->
            if (x = ox && y - 1 = oy) then
                ((x - 1, y), turn dir)
            else
                ((-100, -100), dir)
        | Right ->
            if (x = ox && y + 1 = oy) then
                ((x + 1, y), turn dir)
            else
                ((-100, -100), dir)
        | Unknown -> failwith "Not Implemented"

    let rec isNewStepLooped (map: lab_map) (g: guard) (steps: step array) (ob: position) : bool =
        let res = nextStatus map g
        let (status, moved) = res
        let (p, d) = snd status

        if steps |> Array.exists (fun s -> s = snd status) then
            // printfn "Looped: \n%A, \n%A" status ob
            true
        else if IsCompleted status then
            false
        else if moved then
            isNewStepLooped (fst status) (snd status) (Array.append steps [| (snd status) |]) ob
        else
            isNewStepLooped (fst status) (snd status) steps ob

    let isWorkingObstable (obstacle: position) (steps: step array) (map: lab_map) (g: guard) : bool =
        // get steps in step array around the obstacle
        let newSteps =
            steps |> Array.filter (fun s -> newStep obstacle s |> fst <> (-100, -100))

        // printfn "steps: %A" newSteps

        // check if the new steps are looped
        newSteps
        |> Array.exists (fun s ->
            let (x, y) = obstacle
            map.[x].[y] <- '#'
            let r = isNewStepLooped (map) s [| s |] (x, y)
            map.[x].[y] <- '.'
            r)

    let resolve_part2 (filePath: string) =
        let map = parseInput filePath
        let g = initGuard map
        let steps = moveGuard map g [| g |]

        steps
        |> Array.filter (fun s -> isWorkingObstable (fst s) steps map g)
        |> Array.map (fun s -> fst s)
        |> Array.distinct
        |> Array.length
    // steps
    // |> Array.filter (fun o -> isWorkingObstable (fst o) steps map g)
    // |> Array.length

    let resolve (filePath: string) =
        let map = parseInput filePath
        let g = initGuard map
        let steps = moveGuard map g [| g |]

        steps |> Array.map fst |> Set.ofArray |> Set.count
