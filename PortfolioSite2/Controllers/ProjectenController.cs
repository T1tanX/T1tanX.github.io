using Microsoft.AspNetCore.Mvc;
using PortfolioSite2.DTOs;
using PortfolioSite2.Services;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioSite2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectenController : ControllerBase {
    
    private readonly ProjectenService _projectService;

    public ProjectenController(ProjectenService projectService) {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects() {
        var projecten = await _projectService.GetP();
        return Ok(projecten);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id) {
        var dto = await _projectService.GetOneP(id);
        if (dto  == null) {
            return NotFound();
        }
        return Ok(dto);
    }
    
    [Authorize(Roles = "Admin,Editor")]
    [HttpPost]
    public async Task<ActionResult> PostProject(ProjectInputDto dto) {
        return Ok(new {message = await _projectService.PostP(dto)});
    }
    
    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, ProjectInputDto dto) {
        var success = await _projectService.UpdateP(id, dto);
        if (!success) {
            return NotFound();
        }
        return NoContent();
    }
    
    [Authorize(Roles = "Admin,Editor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id) {
        var success = await _projectService.DeleteP(id);
        if (!success) {
            return NotFound();
        }
        return NoContent();
    }
}