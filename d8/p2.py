#!/usr/bin/env python3
import sys, re

def main():
    instrs = input()
    map = dict()
    for line in sys.stdin:
        line = line.strip()
        match = re.match(r"(\w+) = \((\w+), (\w+)\)", line)
        if not match:
            print(f"Ignoring \"{line}\"")
            continue

        map[match.group(1)] = (match.group(2), match.group(3))
        pass

    cur = [x for x in map.keys() if re.search("A$", x)]
    count = 0
    print(f"{count} {cur}")
    while not all([re.search("Z$", x) for x in cur]):
        if count % 1000 == 0:
            print(f"{count} {cur}")
        for i in range(len(cur)):
            (l, r) = map[cur[i]]

            if instrs[count % len(instrs)] == 'L':
                cur[i] = l
            elif instrs[count % len(instrs)] == 'R':
                cur[i] = r
            else:
                print("Error, invalid instr")

        count += 1
    print(f"{count} {cur}")

if __name__ == "__main__":
    main()
