package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func main() {
    reader := bufio.NewReader(os.Stdin)
    var lines [][]byte
    var extra [][]int
    x, y, i := 0, 0, 0

    if len(os.Args) != 2 {
        fmt.Fprintf(os.Stderr, "usage: %s <number of steps>\n", os.Args[0])
        return;
    }

    n, err := strconv.Atoi(os.Args[1])
    if err != nil {
        fmt.Fprintf(os.Stderr, "usage: %s <number of steps>\n", os.Args[0])
        return;
    }

    for true {
        line, err := reader.ReadBytes('\n')
        if err == nil {
            for j, c := range line {
                if c == 'S' {
                    x = j
                    y = i
                    break
                }
            }
            lines = append(lines, line)
            extra = append(extra, make([]int, len(line)))
        } else {
            break
        }
        i++
    }

    recurse(lines, x, y, n)
    sum := 0
    for _, line := range lines {
        fmt.Print(string(line))
        for _, c := range line {
            if c == 'o' || c == 'O' {
                sum++
            }
        }
    }
    fmt.Printf("x %d y %d n %d result %d\n", x, y, n, sum)
}

func recurse(grid [][]byte, x int, y int, d int) {


    q := []int{x, y, d}

    for len(q) > 0 {
        cx := q[0] // we have queue of int triples at home.
        cy := q[1] // queue of int triples at home.
        cd := q[2]
        q = q[3:]
        if cx < 0 || cy < 0 || cy >= len(grid) || cx >= len(grid[0]) {
            continue
        }

        if grid[cy][cx] == '#' || grid[cy][cx] == '\n' {
            continue
        }

        if grid[cy][cx] == 'O' || grid[cy][cx] == 'P' {
            continue
        }

        if cd == -1 {
            continue
        }
        grid[cy][cx] = min(byte('O' + cd % 2), max(grid[cy][cx], 'P')) // cursed hack. 'O' + 1 = 'P'. no ternaries.
        q = append(q, cx + 1, cy, cd - 1)
        q = append(q, cx - 1, cy, cd - 1)
        q = append(q, cx, cy + 1, cd - 1)
        q = append(q, cx, cy - 1, cd - 1)

    }
    // if extra[y][x] >= d && (extra[y][x] % 2 != d % 2) {
    //     return
    // }
}
