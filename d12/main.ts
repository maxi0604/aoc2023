import { env } from 'process';
import * as rl from 'readline';

const term = rl.createInterface({
    input: process.stdin,
    output: process.stdout,
})

function debug(obj: any) {
    if (env.DEBUG || env.TRACE)
        console.log(obj);
}

function trace(obj: any) {
    if (env.TRACE)
        console.log(obj);
}

function checkFit(field: string, len: number, idx: number, last: boolean) {
    for (let i = idx; i < idx + len; ++i) {
        if (i > field.length || field[i] == '.')
            return false;
    }

    if (idx > 0 && field[idx - 1] == '#')
        return false;

    if (idx + len < field.length && field[idx + len] == '#')
        return false;

    return true;
}

let indentLevel = 0;
function recurse(nums: number[], field: string, idx: number) {
    if (nums.length == 0) {
        return 1;
    }

    let count = 0;
    for (let i = idx; i < field.length; ++i) {
        if (checkFit(field, nums[0], i, nums.length == 1)) {
            indentLevel++;
            let sub = recurse(nums.slice(1), field, i + nums[0] + 1);
            indentLevel--;
            debug(" ".repeat(4 * indentLevel) + `counting ${sub} at ${i} with len = ${nums[0]}`);
            count += sub;
        }
    }
    return count;
}

function doTheThing(lines: string[]) {
    console.log("p1");
    for (const line of lines) {
        const split = line.trim().split(" ").filter(i => i);
        const nums = split[1].split(",").map(x => Number(x));
        const springs = split[0];
        let res = recurse(nums, springs, 0);
        console.log(`${springs} ${res}`);
    }
}

let lines: string[] = [];
term.on("line", (input: string) => { lines.push(input) });
term.on("close", () => doTheThing(lines));
