import { assert } from 'console';
import { env } from 'process';
import * as rl from 'readline';

const term = rl.createInterface({
    input: process.stdin,
    output: process.stdout,
})

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
        // springs = ".............................................................." +  springs + "...................................................................";
        let res = recurse(nums, 0, springs, 0, stack);
        console.log(`${line} ${res}`);
        sum += res;
    }
    console.log(`sum: ${sum}`);
}

let lines: string[] = [];
term.on("line", (input: string) => { lines.push(input); });
term.on("close", () => doTheThing(lines));
