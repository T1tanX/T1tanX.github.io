using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Models;
using PortfolioSite2.DTOs;
using PortfolioSite2.Services;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioSite2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase{
    private readonly BlogService _context;
    
    public BlogController(BlogService context) {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogs() {
        var blog = await _context.GetB();
        return Ok(blog);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneBlog(int id) {
        var b = await _context.GetOneB(id);

        if (b == null) {
            return NotFound(new {message = "Blog not found"});
        }
        
        return Ok(b);
    }
    
    [Authorize(Roles = "Admin, Editor")]
    [HttpPost]
    public async Task<IActionResult> AddBlog(BlogInputDto b) {
        await _context.PostB(b);
        
        return Ok(new {message = "Blog added successfully"});
    }
    
    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog(int id, BlogInputDto b) {
        var success = await _context.UpdateB(id, b);

        if (!success) {
            return NotFound();
        }
        
        return Ok(new {message = "Blog updated successfully"});
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(int id) {
        var findblog = await _context.DeleteB(id);
        
        if(!findblog)
        {
            return  NotFound();
        }
        
        return Ok(new {message = "Blog deleted successfully"});
    }
}