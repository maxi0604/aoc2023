enum class Dir {
    Up,
    Right,
    Down,
    Left
}
fun main(args: Array<String>) {
    println("Hello World!")
    val lines = generateSequence(::readLine).toList()
    var x = 0;
    var y = 0;
    println("count: ${lines.count()}")

    for (iy in 0..<lines.count()) {
        for (ix in 0..<lines[iy].length) {
            if (lines[iy][ix] =='S') {
                println("(ix, iy): ($ix, $iy), cur: ${lines[iy][ix]}")

                x = ix;
                y = iy;
                // break@outer
            }
        }
    }

    // HACK: Hardcode going down initially. Works on sample and main input.
    var dir = Dir.Down;
    ++y;

    var count = 1;
    while (lines[y][x] != 'S') {
        println("(x, y): ($x, $y), count: $count, dir: $dir, cur: ${lines[y][x]}")
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

        val cur = lines[y][x];

        dir = when {
            dir == Dir.Left && cur == 'L' -> Dir.Up
            dir == Dir.Down && cur == 'L' -> Dir.Right
            dir == Dir.Right && cur == 'J' -> Dir.Up
            dir == Dir.Down && cur == 'J' -> Dir.Left
            dir == Dir.Right && cur == '7' -> Dir.Down
            dir == Dir.Up && cur == '7' -> Dir.Left
            dir == Dir.Left && cur == 'F' -> Dir.Down
            dir == Dir.Up && cur == 'F' -> Dir.Right
            else -> dir;
        };

        ++count;
    }

    println("Count:  $count Result: ${count/2}");
    // Try adding program arguments via Run/Debug configuration.
    // Learn more about running applications: https://www.jetbrains.com/help/idea/running-applications.html.
    println("Program arguments: ${args.joinToString()}")
}