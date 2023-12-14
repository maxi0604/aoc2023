import { assert } from 'console';
import { env } from 'process';
import * as rl from 'readline';

const term = rl.createInterface({
    input: process.stdin,
    output: process.stdout,
})

let dp: Map<string, number> = new Map();

function debug(obj: any) {
    if (env.DEBUG || env.TRACE)
        console.log("\t".repeat(indentLevel) + obj);
}

function checkFit(field: string, len: number, idx: number) {
    if (idx + len > field.length)
        return false;

    for (let i = idx; i < idx + len; ++i) {
        if (field[i] == '.')
            return false;
    }

    if (idx > 0 && field[idx - 1] == '#')
        return false;

    if (idx + len < field.length && field[idx + len] == '#')
        return false;

    return true;
}

function checkCovered(field: string, nums: number[], stack: number[]) {
    assert(stack.length == nums.length);
    for (let i = 0; i < field.length; ++i) {
        if (field[i] == '#') {
            let found = false;
            for (let j = 0; j < stack.length; ++j) {
                if (stack[j] <= i && i < stack[j] + nums[j]) {
                    found = true;
                    break;
                }
            }

            if (!found) {
                debug("rejecting " + stack);
                return false;
            }
        }
    }

    return true;
}

let indentLevel = 0;

function recurse(nums: number[], numIdx: number, field: string, idx: number, stack: number[]) {
    let dpKey = `${numIdx}|${idx}`;

    let dpRes = dp.get(dpKey);

    if (dpRes) { // im severely dpRes-sed
        debug(`dp hit: ${dpRes} at ${idx} with len = ${nums[numIdx]}`);
        return dpRes;
    }

    if (nums.length == numIdx) {
        debug("checking final.");
        return checkCovered(field, nums, stack) ? 1 : 0;
    }

    let count = 0;
    for (let i = idx; i < field.length; ++i) {
        if (checkFit(field, nums[numIdx], i)) {
            indentLevel++;
            stack.push(i);
            let sub = recurse(nums, numIdx + 1, field, i + nums[numIdx] + 1, stack);
            stack.pop();
            indentLevel--;
            debug(`counting ${sub} at ${i} with len = ${nums[numIdx]}`);
            count += sub;
        }
    }

    dp.set(dpKey, count);
    return count;
}

function doTheThing(lines: string[]) {
    console.log("p1");
    let sum = 0;
    for (const line of lines) {
        const split = line.trim().split(" ").filter(i => i);
        const nums = split[1].split(",").map(x => Number(x));
        let springs = split[0];
        let stack: number[] = [];
        dp = new Map();
        // springs = ".............................................................." +  springs + "...................................................................";
        let res = recurse(nums, 0, springs, 0, stack);
        console.log(`${line} ${res}`);
        sum += res;
    }
    console.log(`sum: ${sum}`);

    console.log("p2");
    let sum2 = 0;
    for (const line of lines) {
        const split = line.trim().split(" ").filter(i => i);
        const nums = Array(5).fill(split[1].split(",").map(x => Number(x))).flat();
        let springs = Array(5).fill(split[0]).join("?");
        console.log(springs, nums);
        let stack: number[] = [];
        dp = new Map();
        let res = recurse(nums, 0, springs, 0, stack);
        console.log(`${springs} ${nums} ${res}`);
        sum2 += res;
    }
    console.log(`sum2: ${sum2}`);
}

let lines: string[] = [];
term.on("line", (input: string) => { lines.push(input); });
term.on("close", () => doTheThing(lines));
