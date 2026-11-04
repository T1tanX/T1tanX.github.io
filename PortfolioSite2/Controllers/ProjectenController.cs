using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Models;
using PortfolioSite2.DTOs;

namespace PortfolioSite2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectenController : ControllerBase {
    
    private readonly ApplicationDbContext _context;

    public ProjectenController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects() {
        var projecten = await _context.Projects.Select(p => new ProjectDto {
            Id = p.Id,
            Titel = p.Titel,
            Beschrijving = p.Beschrijving,
            Categorie = p.Categorie,
            GitHubUrl = p.GitHubUrl,
            Datum = p.Datum
        }).ToListAsync();
        
        return Ok(projecten);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id) {
        var p = await _context.Projects.FindAsync(id);

        if (p == null) {
            return NotFound();
        }

        var dto = new ProjectDto {
            Id = p.Id,
            Titel = p.Titel,
            Beschrijving = p.Beschrijving,
            Categorie = p.Categorie,
            GitHubUrl = p.GitHubUrl,
            Datum = p.Datum
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> PostProject(ProjectDto dto) {
        var project = new Project {
            Titel = dto.Titel,
            Beschrijving = dto.Beschrijving,
            Categorie = dto.Categorie,
            GitHubUrl = dto.GitHubUrl,
            Datum = dto.Datum
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        dto.Id = project.Id; 
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, ProjectDto dto) {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) {
            return NotFound();
        }

        project.Titel = dto.Titel;
        project.Beschrijving = dto.Beschrijving;
        project.Categorie = dto.Categorie;
        project.GitHubUrl = dto.GitHubUrl;
        project.Datum = dto.Datum;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id) {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}