using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Math;

readonly record struct Mapping(
    string Source,
    string Target,
    long SourceBegin,
    long TargetBegin,
    long Size
);

class Program
{
    public static void Main(string[] argv)
    {
        if (argv.Length > 0 && argv[0].Contains("2"))
        {
            Part2();
        }
        else
        {
            Part1();
        }
    }

    public static void Part1()
    {
        var lines = Console.In.ReadToEnd().Split(Environment.NewLine);
        long[] seeds = lines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToArray();
        List<((string, string), List<(long, long, long)>)> mappings = new();
        Console.WriteLine("read seeds: " + string.Join(", ", seeds));

        // Start at third line.
        string src = "";
        string dst = "";
        foreach (string line in lines.Skip(2))
        {
            Match m;
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            else if ((m = Regex.Match(line, "(\\w+)-to-(\\w+)")).Success)
            {
                src = m.Groups[1].Value;
                dst = m.Groups[2].Value;
                Console.WriteLine($"src: {src}, dst: {dst}");
                mappings.Add(((src, dst), new()));
            }
            else
            {
                long[] map = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToArray();
                Trace.Assert(!string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(dst));
                mappings.Last().Item2.Add((map[0], map[1], map[2]));
                System.Console.WriteLine($"{map[1]}..{map[1] + map[2]} -> {map[0]}..{map[0] + map[2]}");
            }
        }

        long min = long.MaxValue;
        foreach (long seed in seeds)
        {
            long cur = seed;

            foreach ((_, List<(long, long, long)> list) in mappings)
            {
                foreach ((long dstI, long srcI, long len) in list)
                {
                    if (cur >= srcI && cur < srcI + len)
                    {
                        Console.WriteLine($"{cur} -> {cur - srcI + dstI}");
                        cur = cur - srcI + dstI;
                        break;
                    }
                }
            }
            if (cur < min)
            {
                min = cur;
            }
            Console.WriteLine($"mapped seed {seed} -> plot {cur}");
        }
        Console.WriteLine($"min: {min}");
    }

    static bool Valid((long, long) tup) => tup.Item1 < tup.Item2;
    public static void Part2()
    {
        var lines = Console.In.ReadToEnd().Split(Environment.NewLine);
        var seedPairs = lines[0]
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => long.Parse(x))
            .Chunk(2)
            .Select(x => (x[0], x[0] + x[1]))
            .ToArray();
        List<((string, string), List<(long, long, long)>)> mappings = new();

        // Start at third line.
        string src = "";
        string dst = "";
        foreach (string line in lines.Skip(2))
        {
            Match m;
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            else if ((m = Regex.Match(line, "(\\w+)-to-(\\w+)")).Success)
            {
                src = m.Groups[1].Value;
                dst = m.Groups[2].Value;
                Console.WriteLine($"src: {src}, dst: {dst}");
                mappings.Add(((src, dst), new()));
            }
            else
            {
                long[] map = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToArray();
                Trace.Assert(!string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(dst));
                // (dst, src, len) -> (left, right, offset = dst - src)
                var parsed = (map[1], map[1] + map[2], map[0] - map[1]);
                mappings.Last().Item2.Add(parsed);
                System.Console.WriteLine($"{parsed.Item1}..{parsed.Item2} -> {parsed.Item1 + parsed.Item3}..{parsed.Item2 + parsed.Item3}");
            }
        }

        for (int i = 0; i < mappings.Count; ++i)
        {
            (var str, var list) = mappings[i];
            mappings[i] = (str, list.OrderBy(x => x.Item2).ToList());

        }
        long min = long.MaxValue;
        // foreach (var seed in seedPairs)
        // {
            var cur = new List<(long, long)>();
            cur.AddRange(seedPairs);

            foreach (((string from, string to), List<(long, long, long)> list) in mappings)
            {
                Console.WriteLine($"new map {from} -> {to}");
                var next = new List<(long, long)>();

                foreach ((long left, long right, long off) in list) {
                    for (int i = 0; i < cur.Count; i++) {
                        (long curLeft, long curRight) = cur[i];
                        var leftRange = (curLeft, left);
                        var sectRange = (Math.Max(curLeft, left), Math.Min(curRight, right));
                        var mapRange = (Math.Max(curLeft, left) + off, Math.Min(curRight, right) + off);
                        var rightRange = (right, curRight);

                        Console.Write($"{(curLeft, curRight)} -> ");

                        if (Valid(sectRange)) {
                            Console.Write($"sect: {sectRange}, ");
                            if (Valid(leftRange)) {
                                cur.Add(leftRange);
                                Console.Write($"left: {leftRange}, ");
                            }
                            next.Add(mapRange);
                            Console.Write($"map: {sectRange} -> {mapRange}, ");
                            if (Valid(rightRange)) {
                                cur.Add(rightRange);
                                Console.Write($"right: {rightRange}");
                            }

                            cur.Remove((curLeft, curRight));
                            i--;
                            Console.WriteLine();
                        }
                    }
                }

                next.AddRange(cur);
                cur = next;
            }
            foreach ((long left, _) in cur)
            {
                if (left < min) {
                    min = left;
                }
            }

            //Console.WriteLine($"mapped seed {seed} -> plot {cur}");
        // }
        Console.WriteLine($"min: {min}");
    }
}
