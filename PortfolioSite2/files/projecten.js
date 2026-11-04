const container = document.getElementById("projecten-container");
const filterSelect = document.getElementById("taal-filter");

let projectenData = [];

async function laadProjectenVanApi() {
    try {
        const response = await fetch("http://localhost:5050/api/projecten");
        if (!response.ok) {
            throw new Error('Kon geen verbinding maken met de API');
        }

        projectenData = await response.json();
        renderProjecten(projectenData);
    } catch (error) {
        console.error("Fout bij ophalen:", error);
        container.innerHTML = "<p>Kan geen projecten laden uit de database.</p>";
    }
}

function renderProjecten(projectenLijst) {
    if (projectenLijst.length === 0) {
        container.innerHTML = "<p>Geen projecten gevonden.</p>";
        return;
    }

    const htmlInhoud = projectenLijst.map(project => `
        <article class="box">
            <h3>${project.titel}</h3>
            <p>${project.beschrijving}</p>
            <ul>
                <li>Categorie: <strong>${project.categorie}</strong></li>
                <li>Repository: <a href="${project.gitHubUrl}" target="_blank">GitHub Link</a></li>
            </ul>
        </article>
    `).join("");

    container.innerHTML = htmlInhoud;
}

function filterProjecten() {
    const gekozenTaal = filterSelect.value;

    if (gekozenTaal === "alle") {
        renderProjecten(projectenData);
    } else {
        const gefilterd = projectenData.filter(p =>
            p.categorie && p.categorie.toLowerCase().includes(gekozenTaal.toLowerCase())
        );
        renderProjecten(gefilterd);
    }
}

filterSelect.addEventListener("change", filterProjecten);
laadProjectenVanApi();