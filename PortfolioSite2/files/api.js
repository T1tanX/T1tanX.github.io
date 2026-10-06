const factTekst = document.getElementById("fact-tekst");
const factFout = document.getElementById("fact-fout");

function haalFeitjeOp() {
    factTekst.textContent = "Aan het laden...";
    factFout.style.display = "none";
    
    fetch("https://uselessfacts.jsph.pl/api/v2/facts/random")
        .then(function(response) {
            if (!response.ok) {
                throw new Error("Netwerkfout");
            }
            return response.json(); 
        })
        .then(function(data) {
            factTekst.textContent = data.text;
        })
        .catch(function(error) {
            factTekst.style.display = "none";
            factFout.style.display = "block";
            console.error("Fout bij ophalen feitje:", error);
        });
}

haalFeitjeOp();