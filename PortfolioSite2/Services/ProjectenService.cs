using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Models;
using PortfolioSite2.DTOs;
namespace PortfolioSite2.Services;

public class ProjectenService {
    private readonly ApplicationDbContext _context;

    public ProjectenService(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<IEnumerable<ProjectDto>> GetP() {
        var projecten = await _context.Projects.Select(p => new ProjectDto {
            Id = p.Id,
            Titel = p.Titel,
            Beschrijving = p.Beschrijving,
            Categorie = p.Categorie,
            GitHubUrl = p.GitHubUrl,
            Datum = p.Datum
        }).ToListAsync();

        return projecten;
    }
    
    public async Task<ProjectDto?> GetOneP(int id) {
        var p = await _context.Projects.FindAsync(id);

        if (p == null) {
            return null;
        }

        var dto = new ProjectDto {
            Id = p.Id,
            Titel = p.Titel,
            Beschrijving = p.Beschrijving,
            Categorie = p.Categorie,
            GitHubUrl = p.GitHubUrl,
            Datum = p.Datum
        };

        return dto;
    }

    
    public async Task<string> PostP(ProjectInputDto dto) {
        var project = new Project {
            Titel = dto.Titel,
            Beschrijving = dto.Beschrijving,
            Categorie = dto.Categorie,
            GitHubUrl = dto.GitHubUrl,
            Datum = dto.Datum
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return "Project has been added";
    }
    
    
    public async Task<bool> UpdateP(int id, ProjectDto dto) {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return false;

        project.Titel = dto.Titel;
        project.Beschrijving = dto.Beschrijving;
        project.Categorie = dto.Categorie;
        project.GitHubUrl = dto.GitHubUrl;
        project.Datum = dto.Datum;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteP(int id) {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project == null) {
            return false;
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}