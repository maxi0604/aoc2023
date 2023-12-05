using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Math;

readonly record struct Mapping (
    string Source,
    string Target,
    long SourceBegin,
    long TargetBegin,
    long Size
);

class Program {
    public static void Main(string[] argv) {
        if (argv.Length > 0 && argv[0].Contains("2")) {
            Part2();
        } else {
            Part1();
        }
    }

    public static void Part1() {
        var lines = Console.In.ReadToEnd().Split(Environment.NewLine);
        long[] seeds = lines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToArray();
        List<((string, string), List<(long, long, long)>)> mappings = new();
        Console.WriteLine("read seeds: " + string.Join(", ", seeds));

        // Start at third line.
        string src = "";
        string dst = "";
        foreach (string line in lines.Skip(2)) {
            Match m;
            if (string.IsNullOrWhiteSpace(line)) {
                continue;
            } else if ((m = Regex.Match(line, "(\\w+)-to-(\\w+)")).Success) {
                src = m.Groups[1].Value;
                dst = m.Groups[2].Value;
                Console.WriteLine($"src: {src}, dst: {dst}");
                mappings.Add(((src, dst), new()));
            } else {
                long[] map = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToArray();
                Trace.Assert(!string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(dst));
                mappings.Last().Item2.Add((map[0], map[1], map[2]));
                System.Console.WriteLine($"{map[1]}..{map[1] + map[2]} -> {map[0]}..{map[0] + map[2]}");
            }
        }

        long min = long.MaxValue;
        foreach (long seed in seeds) {
            long cur = seed;

            foreach((_, List<(long, long, long)> list) in mappings) {
                foreach ((long dstI, long srcI, long len) in list) {
                    if (cur >= srcI && cur < srcI + len) {
                        Console.WriteLine($"{cur} -> {cur - srcI + dstI}");
                        cur = cur - srcI + dstI;
                        break;
                    }
                }
            }
            if (cur < min) {
                min = cur;
            }
            Console.WriteLine($"mapped seed {seed} -> plot {cur}");
        }
        Console.WriteLine($"min: {min}");
    }

    public static void Part2() {
        var lines = Console.In.ReadToEnd().Split(Environment.NewLine);
        long[][] seedPairs = lines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).Chunk(2).ToArray();
        List<((string, string), List<(long, long, long)>)> mappings = new();

        // Start at third line.
        string src = "";
        string dst = "";
        foreach (string line in lines.Skip(2)) {
            Match m;
            if (string.IsNullOrWhiteSpace(line)) {
                continue;
            } else if ((m = Regex.Match(line, "(\\w+)-to-(\\w+)")).Success) {
                src = m.Groups[1].Value;
                dst = m.Groups[2].Value;
                Console.WriteLine($"src: {src}, dst: {dst}");
                mappings.Add(((src, dst), new()));
            } else {
                long[] map = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToArray();
                Trace.Assert(!string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(dst));
                mappings.Last().Item2.Add((map[0], map[1], map[2]));
                System.Console.WriteLine($"{map[1]}..{map[1] + map[2]} -> {map[0]}..{map[0] + map[2]}");
            }
        }

        for (int i = 0; i < mappings.Count; ++i) {
            (var str, var list) = mappings[i];
            mappings[i] = (str, list.OrderBy(x => x.Item2).ToList());

        }
        long min = long.MaxValue;
        foreach (long[] seedPair in seedPairs) {
            List<long[]> cur = new List<long[]>();
            cur.Add(seedPair);
            foreach((_, List<(long, long, long)> list) in mappings) {
                List<long[]> next = new List<long[]>();
                foreach (long[] curPair in cur) {
                    Console.WriteLine($"mapping {curPair[0]}..{curPair[0] + curPair[1]}.");
                    foreach ((long dstI, long srcI, long len) in list) {
                        // represented excluding right including left bound.
                        long curLeft = curPair[0];
                        long curRight = curPair[0] + curPair[1];
                        long mapLeft = srcI;
                        long mapRight = srcI + len;
                        // In (left, length) notation again.
                        long[] left = new long[] {curLeft, mapLeft - curLeft};
                        long[] toMap = new long[] {Max(curLeft, mapLeft), Min(curRight, mapRight) - Max(curLeft, mapLeft) };
                        long newLeft = Min(curRight, mapRight) + 1;
                        long newCurLen = curPair[1] - Max(left[1], 0) - Max(toMap[1], 0);

                        if (left[1] > 0)
                            next.Add(left);

                        Console.WriteLine($"splitting {curPair[0]}..{curPair[1] + curPair[0]} into left: {left[0]}..{left[0] + left[1]}, map: {toMap[0]}..{toMap[0] + toMap[1]} -> {}, right: {newLeft}..{newLeft + newCurLen}");
                        if (toMap[1] > 0) {
                            toMap[0] = toMap[0] - srcI + dstI;
                        }

                        curPair[0] = newLeft;
                        curPair[1] = newCurLen;
                    }

                    if (curPair[1] > 0) {
                        next.Add(curPair);
                    }
                }
                cur = next;
            }

            for (int i = 0; i < cur.Count; i++) {
                if (cur[i][0] < min) {
                    min = cur[i][0];
                }
            }

            //Console.WriteLine($"mapped seed {seed} -> plot {cur}");
        }
        Console.WriteLine($"min: {min}");
    }
}
