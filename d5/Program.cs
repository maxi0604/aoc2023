using System.Text.RegularExpressions;
using System.Diagnostics;

readonly record struct Mapping (
    string Source,
    string Target,
    int SourceBegin,
    int TargetBegin,
    int Size
);

class Program {
    public static void Main(string[] argv) {
        var lines = Console.In.ReadToEnd().Split(Environment.NewLine);
        int[] seeds = lines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => int.Parse(x)).ToArray();
        List<((string, string), List<(int, int, int)>)> mappings = new();
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
                int[] map = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => int.Parse(x)).ToArray();
                Trace.Assert(!string.IsNullOrWhiteSpace(src) && !string.IsNullOrWhiteSpace(dst));
                mappings.Last().Item2.Add((map[0], map[1], map[2]));
                System.Console.WriteLine($"{map[1]}..{map[1] + map[2]} -> {map[0]}..{map[0] + map[2]}");
            }
        }

        int min = int.MaxValue;
        foreach (int seed in seeds) {
            int cur = seed;

            foreach((_, List<(int, int, int)> list) in mappings) {
                foreach ((int dstI, int srcI, int len) in list) {
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
    }
}
