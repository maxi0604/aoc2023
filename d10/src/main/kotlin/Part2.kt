fun main(args: Array<String>) {
    println("Hello World!")
    val lines = generateSequence(::readLine).map { it.toCharArray() }.toList()
    var part = Array(lines.size) { BooleanArray(lines[0].size) }
    var x = 0;
    var y = 0;
    println("count: ${lines.count()}")

    for (iy in 0..<lines.count()) {
        for (ix in 0..<lines[iy].count()) {
            if (lines[iy][ix] =='S') {
                println("(ix, iy): ($ix, $iy), cur: ${lines[iy][ix]}")

                x = ix;
                y = iy;
                // break@outer
            }
        }
    }


    // HACK: Hardcode going down once initially. Works on sample and main input.
    part[y][x] = true;
    var dir = Dir.Down;
    ++y;

    while (lines[y][x] != 'S') {
        part[y][x] = true;
        println("(x, y): ($x, $y), dir: $dir, cur: ${lines[y][x]}")

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
    }

    println("with loop");
    for (line in lines) {
        println(String(line))
    }

    var nestSize = 0;
    for (line in lines) {
        var inside = false;

        for (i in line.indices) {
            val c = line[i];
            if (c == '#')
                inside = !inside;
            else if (c == '.' && inside) {
                ++nestSize;
                line[i] = 'N';
            }
        }
    }
    println("with nest (row-major)");
    for (line in lines) {
        println(String(line))
    }

    for (col in lines[0].indices) {
        var inside = false;

        for (row in lines.indices) {
            val c = lines[row][col];
            if (c == '#')
                inside = !inside;
            else if (c == 'N' && !inside) {
                lines[row][col] = ',';
                --nestSize;
            }
        }
    }

    println("with nest");
    for (line in lines) {
        println(String(line))
    }

    println("nest size: $nestSize");
    // Try adding program arguments via Run/Debug configuration.
    // Learn more about running applications: https://www.jetbrains.com/help/idea/running-applications.html.
    println("Program arguments: ${args.joinToString()}")
}