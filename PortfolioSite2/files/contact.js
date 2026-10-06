const form = document.getElementById("contact-form");

form.addEventListener("submit", function(event) {
    event.preventDefault(); 

    const naam = document.getElementById("naam").value.trim();
    const email = document.getElementById("email").value.trim();
    const bericht = document.getElementById("bericht").value.trim();

    const naamFout = document.getElementById("naam-fout");
    const emailFout = document.getElementById("email-fout");
    const berichtFout = document.getElementById("bericht-fout");
    const succesMelding = document.getElementById("succes-melding");
    
    naamFout.textContent = "";
    emailFout.textContent = "";
    berichtFout.textContent = "";
    succesMelding.style.display = "none";

    let isGeldig = true;

    if (naam === "") {
        naamFout.textContent = "Vul je naam in.";
        isGeldig = false;
    }

    if (email === "" || !email.includes("@")) {
        emailFout.textContent = "Vul een geldig e-mailadres in.";
        isGeldig = false;
    }

    if (bericht.length < 10) {
        berichtFout.textContent = "Het bericht moet minimaal 10 tekens lang zijn.";
        isGeldig = false;
    }
    
    if (isGeldig) {
        succesMelding.style.display = "block";
        form.reset();
    }
});