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

    cur = "AAA"
    count = 0
    while cur != "ZZZ":
        print(f"{count} {cur}")
        (l, r) = map[cur]
        if instrs[count % len(instrs)] == 'L':
            cur = l
        elif instrs[count % len(instrs)] == 'R':
            cur = r
        else:
            print("Error, invalid instr")

        count += 1
    print(f"{count} {cur}")

if __name__ == "__main__":
    main()
