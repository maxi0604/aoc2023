using System.Text.RegularExpressions;
using System.Diagnostics;

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

        long min = long.MaxValue;
        foreach (long[] seedPair in seedPairs) {
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
            //Console.WriteLine($"mapped seed {seed} -> plot {cur}");
        }
        Console.WriteLine($"min: {min}");
    }
}
