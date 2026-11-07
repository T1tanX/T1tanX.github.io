using Microsoft.EntityFrameworkCore;
using PortfolioSite2.DTOs;
using PortfolioSite2.Models;

namespace PortfolioSite2.Services;

public class BlogService {
    private readonly ApplicationDbContext _context;

    public BlogService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<BlogDto>> GetB() {
        var blog = await _context.Blogs.Select(bl => new BlogDto {
            Id = bl.Id,
            Titel = bl.Titel,
            Datum = bl.Datum,
            Inhoud = bl.Inhoud,
        }).ToListAsync();
        
        return blog;
    }
    
    public async Task<BlogDto?> GetOneB(int id) {
        var b = await _context.Blogs.FindAsync(id);

        if (b == null) {
            return null;
        }

        var dto = new BlogDto() {
            Id = b.Id,
            Titel = b.Titel,
            Datum = b.Datum,
            Inhoud = b.Inhoud
        };

        return dto;
    }

    
    public async Task PostB(BlogInputDto dto) {
        var blog = new Blog() {
            Titel = dto.Titel,
            Datum = dto.Datum,
            Inhoud = dto.Inhoud
        };

        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<bool> UpdateB(int id, BlogDto dto) {
        var searchedBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        if (searchedBlog == null) {
            return false;
        }
        
        searchedBlog.Titel = dto.Titel;
        searchedBlog.Datum = dto.Datum;
        searchedBlog.Inhoud = dto.Inhoud;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteB(int id) {
        var searchedblog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        if (searchedblog == null) {
            return false;
        }

        _context.Blogs.Remove(searchedblog);
        await _context.SaveChangesAsync();
        return true;
    }
}