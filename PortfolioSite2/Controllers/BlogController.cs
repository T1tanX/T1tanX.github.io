using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Models;
using PortfolioSite2.DTOs;

namespace PortfolioSite2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase{
    private readonly ApplicationDbContext _context;
    
    public BlogController(ApplicationDbContext context) {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs() {
        var blog = await _context.Blogs.Select(b => new BlogDto {
            Id = b.Id,
            Titel = b.Titel,
            Datum = b.Datum,
            Inhoud = b.Inhoud
        }).ToListAsync();
        
        return Ok(blog);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneBlog(int id) {
        var blog = await _context.Blogs.FirstOrDefaultAsync(bl => bl.Id == id);

        if (blog == null) {
            return NotFound(new {message = "Blog id not found"});
        }
        
        return Ok(blog);
    }

    [HttpPost]
    public async Task<ActionResult> AddBlog([FromBody] BlogDto b) {
        var blog = new Blog{
            Id = b.Id,
            Titel = b.Titel,
            Datum = b.Datum,
            Inhoud = b.Inhoud
        };

        _context.Blogs.Add(blog);
        await  _context.SaveChangesAsync();
        
        return Ok(new {message =  "Blog added successfully"});
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBlog(int id, [FromBody] BlogDto b) {
        var findblog = await _context.Blogs.FirstOrDefaultAsync(bl => bl.Id == id);

        if (findblog == null) {
            return NotFound(new {message= "Blog id not found"});
        }

        findblog.Titel = b.Titel;
        findblog.Datum = b.Datum;
        findblog.Inhoud = b.Inhoud;
        
        await  _context.SaveChangesAsync();
        return Ok(new {message = "Blog updated successfully"});
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBlog(int id) {
        var findblog = await _context.Blogs.FirstOrDefaultAsync(bl => bl.Id == id);

        if (findblog == null) {
            return NotFound(new {message= "Blog id not found"});
        }

        _context.Blogs.Remove(findblog);
        await _context.SaveChangesAsync();
        
        return Ok(new {message = "Blog deleted successfully"});
    }
}