const projectenData = [
    {
        titel: "Pokemon Console Game",
        beschrijving: "Deze Pokemon console game is iets waar ik 2 weken aan heb gewerkt.\n" +
            "               Het is voor nu nog een simpele one-vs-one turned based game waarbij elke speler een pokemon \n" +
            "               kan kiezen uit een lijst. Je een pokemon uit deze lijst opzoeken op naam of je kunt de lijst \n" +
            "               filteren op pokemon type. Elke pokemon heeft een vast moveset, je kunt dus helaas nog niet voordat\n" +
            "               de battle begint een aantal moves naar keuze kiezen. het battle systeem houdt, met behulp van functies, goed\n" +
            "               in de gaten of moves niet effectief, effectief, super effectief of neutraal zijn tegen de pokemon van \n" +
            "               de tegenstander en past op basis daarvan de damage van jouw gekozen move aan met behulp van multipliers.\n" +
            "               ook houd het system de HP van jouw Pokemon bij en geeft deze weer na elke turn.\n" +
            "               Ik ben zeker van plan om deze game in de toekomst verder uit te breiden en er mischien zelfs een visueel \n" +
            "               game van te maken, omdat ik mijn game development vaardigheden naar een hoger niveau wil brengen.\n" +
            "                Helaas kan ik het dan niet publishen (copyright) :(.",
        talen: ["C#"],
        repoUrl: "https://github.com/T1tanX/PokemonConsoleGame.git",
        repoNaam: "PokemonConsoleGame"
    },
    {
        titel: "Neural Network Modellen",
        beschrijving: "In dit project heb ik 3 neural network modellen gemaakt die elk lagen van neuronen gebruiken om \n" +
            "               complexe data stap voor stap te ontleden.\n" +
            "               Het eerste model is een space shooter stijl game dat \n" +
            "               gespeeld wordt door een AI op basis van traindata die verzameld werd toen ik de game had gespeeld. Dit model \n" +
            "               gebruikt clustering om data te groeperen.\n" +
            "               Het tweede model probeert zo accuraat mogelijk huisprijzen te voorspellen op basis van features \n" +
            "               uit een dataset. Vervolgens vergelijkt het model de voorspelde huis prijzen met de daadwerkelijke huis prijzen\n" +
            "               uit de dataset om te bepalen hoe accuraat de voorspelde huisprijs is. Dit model maakt gebruik van\n" +
            "               lineaire regressie.\n" +
            "               Het derde model probeert wijnen uit 2 verschillende datasets in 2 groepen te plaatsen, rode wijn en \n" +
            "               witte wijn. Dit model gebruikt hiervoor net zo als het eerste model ook clustering.",
        talen: ["Python"],
        repoUrl: "https://github.com/T1tanX/NeuralNetworkModels.git",
        repoNaam: "NeuralNetworkModels"
    },
    {
        titel: "Datawarehouse",
        beschrijving: "Dit was een school project waarbij ik in een duo een datawarehouse moest bouwen. In dit project wordt data uit 5 \n" +
            "               verschillende source databases genormaliseerd en ingeladen via een data pipeline in een source data model die je kunt openen \n" +
            "               in DB browser (SQlite). Daarna wordt de data vanuit het source data model gedenormaliseerd en ingeladen via een andere data pipeline \n" +
            "               in een datawarehouse die je ook kunt openen in DB browser (SQlite). De data in het datawarehouse is gereed voor \n" +
            "               analyse. Je kunt dus een platform zoals Power BI gebruiken om je data uit het datawarehouse te visualiseren in \n" +
            "               allerlei grafieken of schema's.",
        talen: ["Python", "SQL"],
        repoUrl: "https://github.com/Taigaverybig/DuoTariq-Hubert.git",
        repoNaam: "Datawarehouse"
    },
    {
        titel: "Trainingsschema Generator",
        beschrijving: "Een individueel project dat willekeurige trainingsschema's genereert op basis van gekozen specificaties, spiergroepen en moeilijkheidsgraad.",
        talen: ["Java"],
        repoUrl: "https://github.com/T1tanX/Trainingsschema-generator.git",
        repoNaam: "TrainingsschemaGenerator"
    }
];

const container = document.getElementById("projecten-container");
const filterSelect = document.getElementById("taal-filter");


function renderProjecten(projectenLijst) {
    if (projectenLijst.length === 0) {
        container.innerHTML = "<p>Geen projecten gevonden voor deze taal.</p>";
        return;
    }
    
    const htmlInhoud = projectenLijst.map(project => `
        <article class="box">
            <h3>${project.titel}</h3>
            <p>${project.beschrijving}</p>
            <ul>
                <li>Programmeer taal: <strong>${project.talen.join(", ")}</strong></li>
                <li>Repository: <a href="${project.repoUrl}">${project.repoNaam}</a></li>
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
        const gefilterd = projectenData.filter(p => p.talen.includes(gekozenTaal));
        renderProjecten(gefilterd);
    }
}


filterSelect.addEventListener("change", filterProjecten);
renderProjecten(projectenData);