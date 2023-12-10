fun flipAfter(a: Char, b: Char): Boolean {
    return when {
        a == 'L' && b == '7' -> true
        a == 'F' && b == 'J' -> true
        else -> false
    }
}
fun main(args: Array<String>) {
    println("Hello World!")
    val lines = generateSequence(::readLine).map { it.toCharArray() }.toList()
    var part = Array(lines.size) { BooleanArray(lines[0].size) }
    var x = 0
    var y = 0
    println("count: ${lines.count()}")

    for (iy in 0..<lines.count()) {
        for (ix in 0..<lines[iy].count()) {
            if (lines[iy][ix] =='S') {
                println("(ix, iy): ($ix, $iy), cur: ${lines[iy][ix]}")

                x = ix
                y = iy
                // break@outer
            }
        }
    }


    // HACK: Hardcode going down once initially. Works on sample and main input.
    part[y][x] = true
    var dir = Dir.Down

     do {
        part[y][x] = true
        println("(x, y): ($x, $y), dir: $dir, cur: ${lines[y][x]}")
        // lines[y][x] = '#'
        x = when(dir) {
            Dir.Left -> x - 1
            Dir.Right -> x + 1
            else -> x
        }

        y = when(dir) {
            Dir.Up -> y - 1
            Dir.Down -> y + 1
            else -> y
        }

        val cur = lines[y][x]

        dir = when {
            dir == Dir.Left && cur == 'L' -> Dir.Up
            dir == Dir.Down && cur == 'L' -> Dir.Right
            dir == Dir.Right && cur == 'J' -> Dir.Up
            dir == Dir.Down && cur == 'J' -> Dir.Left
            dir == Dir.Right && cur == '7' -> Dir.Down
            dir == Dir.Up && cur == '7' -> Dir.Left
            dir == Dir.Left && cur == 'F' -> Dir.Down
            dir == Dir.Up && cur == 'F' -> Dir.Right
            else -> dir
        }
    } while (lines[y][x] != 'S')

    // More strikingly, in all cases 'S' can be replaced by 'F' or '7'
    lines[y][x] = when (lines[y][x - 1]) {
        'F', 'L', '-' -> '7'
        else -> 'F'
    }

    println("with loop")


    var nestSize = 0
    for (j in lines.indices) {
        var inside = false
        var pair = '.'

        for (i in lines[j].indices) {
            val c = lines[j][i]
            if (part[j][i]) {
                if (c == '|')
                    inside = !inside
                else if (flipAfter(pair, c)) {
                    println("flip: $pair $c")
                    inside = !inside
                    pair = '.'
                }
                else if (c == 'L' || c == 'J' || c == '7' || c == 'F') {
                    pair = when (pair) {
                        '.' -> c;
                        else -> '.';
                    }
                    println("set pair: $pair")
                }
            }
            else if (inside) {
                ++nestSize
                lines[j][i] = 'N'
            }
        }
        println(String(lines[j]))
    }
    println("with nest (row-major)")
    for (line in lines) {
        println(String(line))
    }

    println("nest size: $nestSize")
}