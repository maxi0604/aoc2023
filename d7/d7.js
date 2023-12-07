
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
    for (const line of lines) {
        let parsed = line.split(" ");
        if (parsed.length < 2) {
            continue;
        }
        let sorted = Array.from(parsed[0]).sort().join("");
    }


}
animate();
