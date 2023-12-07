
function animate() {
    for (let i = 0; i < 500; i++) {
        let parent = document.getElementById("annoying-animation");
        let flake = document.createElement("div");
        flake.classList.add("flake");
        flake.innerText = "❄️";
        flake.style.left = `${Math.random() * 100}vw`;
        flake.style.animationDuration = `${Math.random() * 5 + 7}vw`;
        flake.style.animationDelay = `${-15 * Math.random()}s`;
        parent.appendChild(flake);
    }
}

function doTheThing() {
    console.log("did the thing");
    let textField = document.getElementById("in");
    let lines = textField.value.split("\n");
    let values = [];

    for (const line of lines) {
        let lexed = line.split(" ");
        if (lexed.length < 2) {
            setErr(`Invalid line: "${line}"`);
            return;
        }

        values.push([judge(lexed[0]), Number(lexed[1])]);
    }

    values.sort();

    let curRank = 1;
    let curScore = 0;
    let curSum = 0;
    for (let i = 0; i < values.length; i++) {
        if (values[i][0] > curScore) {
            curScore = values[i][0];
            curRank++;
        }

        curSum += curRank * values[i][1];
    }

    setResult(curSum);
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
        return 5;

    if (sorted.match(/(.)\1{3}/))
        return 4;

    if (sorted.match(/(.)\1{2}(.)\2/) || sorted.match(/(.)\1(.)\2{2}/))
        return 3;

    if (sorted.match(/(.)\1{2}/))
        return 2;

    if (sorted.match(/(.)\1(.)\2/) || sorted.match(/(.)\1.(.)\2/))
        return 2;

    if (sorted.match(/(.)\1(.)\2/))
        return 1;

    return 0;
}
animate();
