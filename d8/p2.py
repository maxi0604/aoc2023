#!/usr/bin/env python3
import sys, re, math

def main():
    instrs = input()
    map = dict()
    dp = dict()
    for line in sys.stdin:
        line = line.strip()
        match = re.match(r"(\w+) = \((\w+), (\w+)\)", line)
        if not match:
            print(f"Ignoring \"{line}\"")
            continue

        map[match.group(1)] = (match.group(2), match.group(3))
        pass

    cur = [x for x in map.keys() if re.search("A$", x)]
    curResult = [0 for x in range(len(cur))]
    count = 0
    print(f"{count} {cur}")
    while not all(curResult):
        # if count % 1000 == 0:
        print(f"{count} {cur}")
        for i in range(len(cur)):
            if curResult[i]:
                continue

            if re.search("Z$", cur[i]):
                curResult[i] = count

            (l, r) = map[cur[i]]

            if instrs[count % len(instrs)] == 'L':
                cur[i] = l
            elif instrs[count % len(instrs)] == 'R':
                cur[i] = r
            else:
                print("Error, invalid instr")

        count += 1
    print(f"{count} {cur}")
    print(f"{curResult} lcm: {math.lcm(*curResult)}")

if __name__ == "__main__":
    main()
