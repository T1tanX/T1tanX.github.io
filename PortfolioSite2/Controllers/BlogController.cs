using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Models;
using PortfolioSite2.DTOs;
using PortfolioSite2.Services;

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

    [HttpPost]
    public async Task<ActionResult> AddBlog(BlogInputDto b) {
        await _context.PostB(b);
        
        return Ok(new {message = "Blog added successfully"});
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBlog(int id, BlogDto b) {
        var success = await _context.UpdateB(id, b);

        if (!success) {
            return NotFound();
        }
        
        return Ok(new {message = "Blog updated successfully"});
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBlog(int id) {
        var findblog = await _context.DeleteB(id);
        
        if(!findblog)
        {
            return  NotFound();
        }
        
        return Ok(new {message = "Blog deleted successfully"});
    }
}