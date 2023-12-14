import * as rl from 'readline';

const term = rl.createInterface({
    input: process.stdin,
    output: process.stdout,
})

function checkFit(field: string, len: number, idx: number) {
    return !!field.slice(Math.max(idx - 1, 0), Math.min(idx + len + 1, field.length)).match(/^[.?][#?]+[.?]?$/);
}

let indentLevel = 0;
function recurse(nums: number[], field: string, idx: number) {
    if (nums.length == 0) {
        return 1;
    }

    let count = 0;
    for (let i = idx; i < field.length; ++i) {
        if (checkFit(field, nums[0], i)) {
            indentLevel++;
            let sub = recurse(nums.slice(1), field, i + nums[0] + 1);
            indentLevel--;
            console.log(" ".repeat(4 * indentLevel) + `counting ${sub} at ${i} with len = ${nums[0]}`);
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
        console.log(nums, springs);
        console.log(recurse(nums, springs, 0));
    }
}

let lines: string[] = [];
term.on("line", (input: string) => { lines.push(input) });
term.on("close", () => doTheThing(lines));
