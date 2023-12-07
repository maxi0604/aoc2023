let letterToNum = {
    "A": 14,
    "K": 13,
    "Q": 12,
    "J": 11,
    "T": 10,
    "9": 9,
    "8": 8,
    "7": 7,
    "6": 6,
    "5": 5,
    "4": 4,
    "3": 3,
    "2": 2,
};

function animate() {
    for (let i = 0; i < 250; i++) {
        let parent = document.getElementById("annoying-animation");
        let flake = document.createElement("div");
        flake.classList.add("flake");
        flake.innerText = "❄️";
        flake.style.left = `${Math.random() * 100}vw`;
        flake.style.animationDuration = `${Math.random() * 7 + 5}vw`;
        flake.style.animationDelay = `${-15 * Math.random()}s`;
        parent.appendChild(flake);
    }
}

function doTheThing() {
    let part1 = document.getElementById("part1").checked;
    let part2 = document.getElementById("part2").checked;

    if (!(part1 || part2)) {
        setErr("Select part.");
        return;
    }

    if (part1) {
        letterToNum["J"] = 11;
    }
    else {
        letterToNum["J"] = 1;
    }

    let textField = document.getElementById("in");
    let lines = textField.value.split("\n");
    let values = [];

    for (const line of lines) {
        let lexed = line.split(" ");
        if (lexed.length < 2) {
            setErr(`Invalid line: "${line}"`);
            return;
        }

        var judgement;
        if (part1) {
            judgement = judge(lexed[0]);
        } else {
            let options = [];
            for (letter in letterToNum) {
                options.push(judge(lexed[0].replaceAll("J", letter)));
            }
            judgement = Math.max(...options);
        }

        values.push([judgement, Array.from(lexed[0]).map(x => letterToNum[x]), Number(lexed[1]), lexed[0]]);
    }

    values.sort((a, b) => {
        if (a[0] != b[0])
            return a[0] - b[0];

        return arrCompare(a[1], b[1]);
    });

    let sum = 0;
    for (let i = 0; i < values.length; i++) {
        sum += (i + 1) * values[i][2];
    }

    setResult(sum);
}

function arrCompare(a, b) {
    for (let i = 0; i < 5; i++) {
        if (a[i] < b[i]) {
            return -1;
        }
        else if (a[i] > b[i]) {
            return 1;
        }
    }

    return 0;
}

function setResult(res) {
    document.getElementById("result").innerText = `Result: ${res}`;
}

function setErr(err) {
    document.getElementById("result").innerText = `Error: ${err}`;
}

function judge(str) {
    let sorted = Array.from(str).sort().join("");

    if (sorted.match(/(.)\1{4}/))
        return 6;

    if (sorted.match(/(.)\1{3}/))
        return 5;

    if (sorted.match(/(.)\1{2}(.)\2/) || sorted.match(/(.)\1(.)\2{2}/))
        return 4;

    if (sorted.match(/(.)\1{2}/))
        return 3;

    if (sorted.match(/(.)\1(.)\2/) || sorted.match(/(.)\1.(.)\2/))
        return 2;

    if (sorted.match(/(.)\1/))
        return 1;

    return 0;
}

animate();
