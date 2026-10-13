const form = document.getElementById("contact-form");

function checkValidity (value, valueFout, type){
    let confirm = true;
    
    switch (type){
        case "naam":
            if (value === "") {
                valueFout.textContent = "Vul je naam in.";
                confirm = false;
            }
            break;
        case "email":
            if (value === "" || !(value.includes("@") && value.endsWith(".com") && value === value.toLowerCase())) {
                valueFout.textContent = "Vul een geldig e-mailadres in (@, .com, geen hoofdletters).";
                confirm = false;
            } 
            break;
        case "bericht":
            if (value.length < 10) {
                valueFout.textContent = "Het bericht moet minimaal 10 tekens lang zijn.";
                confirm = false;
            }
    }
    return confirm;
}
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
    
    const naamConfirm = checkValidity(naam, naamFout, "naam")
    const emailConfirm = checkValidity(email, emailFout, "email")
    const berichtConfirm = checkValidity(bericht, berichtFout, "bericht")
    
    if (naamConfirm && emailConfirm && berichtConfirm){
        succesMelding.style.display = "block";
        form.reset();
    }
});