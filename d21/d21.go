package main

import (
	"bufio"
	"fmt"
	"os"
	"slices"
	"strconv"
)

func main() {
    reader := bufio.NewReader(os.Stdin)
    var lines [][]byte
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
        } else {
            break
        }
        i++
    }

    recurse(lines, x, y, n, make([]int, 0))
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

func recurse(grid [][]byte, x int, y int, d int, stack []int) {
    if x < 0 || y < 0 || y >= len(grid) || x >= len(grid[0]) {
        return
    }

    if grid[y][x] == '#' || grid[y][x] == '\n' {
        return
    }

    // We have int tuples at home. Int tuples at home:
    if slices.Contains(stack, x << 16 | y) {
        return
    }
    if d != 0 {
        tmp := make([]int, len(stack) + 1)
        copy(tmp, stack)
        tmp = append(tmp, x << 16 | y)
        recurse(grid, x + 1, y, d - 1, tmp)
        recurse(grid, x - 1, y, d - 1, tmp)
        recurse(grid, x, y + 1, d - 1, tmp)
        recurse(grid, x, y - 1, d - 1, tmp)
        if d % 2 == 0 {
            grid[y][x] = 'O'
        } else {
            grid[y][x] = 'P'
        }
        return
    }

    grid[y][x] = 'o'
}
