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

    const naam = document.getElementById("naam")
    const email = document.getElementById("email")
    const bericht = document.getElementById("bericht")

    const naamFout = document.getElementById("naam-fout");
    const emailFout = document.getElementById("email-fout");
    const berichtFout = document.getElementById("bericht-fout");
    const succesMelding = document.getElementById("succes-melding");
    
    let naamType = naam.id;
    let emailType = email.id;
    let berichtType = bericht.id;
    
    const naamText = naam.value.trim();
    const berichtText = bericht.value.trim();
    const emailText = email.value.trim();
    
    naamFout.textContent = "";
    emailFout.textContent = "";
    berichtFout.textContent = "";
    succesMelding.style.display = "none";
    
    const naamConfirm = checkValidity(naamText, naamFout, naamType)
    const emailConfirm = checkValidity(emailText, emailFout, emailType)
    const berichtConfirm = checkValidity(berichtText, berichtFout, berichtType)
    
    if (naamConfirm && emailConfirm && berichtConfirm){
        succesMelding.style.display = "block";
        form.reset();
    }
});