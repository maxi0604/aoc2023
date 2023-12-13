import * as rl from 'readline';

const term = rl.createInterface({
    input: process.stdin,
    output: process.stdout,
})

let lines: string[] = [];
term.on("line", (input: string) => { lines.push(input) });
term.on("close", () => {
    console.log("p1");
    for (const line of lines) {
        const split = line.trim().split(" ").filter(i => i);
        const nums = split[1].split(",").map(x => Number(x));
        const springs = split[0];
        console.log(nums, springs);
    }
});
