const apiUrl = "http://localhost:5050/api/blog";
const container = document.getElementById("blogs-container");

async function laadBlogs() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error("Kon de blogs niet ophalen.");
        }

        const blogsLijst = await response.json();
        renderBlogs(blogsLijst);
    } catch (error) {
        console.error("Fout:", error);
        container.innerHTML = "<p>Er is een fout opgetreden bij het laden van de blogs.</p>";
    }
}

function renderBlogs(blogsLijst) {
    if (blogsLijst.length === 0) {
        container.innerHTML = "<p>Geen blog posts gevonden.</p>";
        return;
    }

    const htmlInhoud = blogsLijst.map(blog => {
        const Datum = new Date(blog.datum).toLocaleDateString('nl-NL', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
        
        return `
            <article class="box">
                <h3>${blog.titel}</h3>
                <p><small>geplaatst op: ${Datum}</small></p>
                <p>${blog.inhoud}</p>
            </article>
        `;
    }).join("");

    container.innerHTML = htmlInhoud;
}

laadBlogs();